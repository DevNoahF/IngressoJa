import { useEffect, useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './PaymentPage.css';
import Footer from '../../components/Home/Footer';
import qrcodepix from '../../assets/qrcodepix.png';
import HeaderUser from '../../components/HeaderUser/HeaderUser';
import { getEventById } from '../../api/events';
import { approveSale, createSale } from '../../api/sales';
import { getStoredUserId } from '../../utils/auth';
import { clearStoredEventId, getStoredEventId } from '../../utils/eventContext';

function isGuid(value) {
  return /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(value);
}

export default function PaymentPage() {
  const navigate = useNavigate();
  const [step, setStep] = useState('checkout');
  const [quantidade, setQuantidade] = useState(1);
  const [event, setEvent] = useState(null);
  const [sale, setSale] = useState(null);
  const [ticketCode, setTicketCode] = useState('');
  const [pageLoading, setPageLoading] = useState(true);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const userId = getStoredUserId();
  const eventId = getStoredEventId();
  const valorUnitario = Number(event?.ticketValue ?? 0);
  const quantidadeMaxima = Math.max(1, Number(event?.totalTicketQuantity ?? 1));
  const valorTotal = useMemo(() => quantidade * valorUnitario, [quantidade, valorUnitario]);

  useEffect(() => {
    let isMounted = true;

    async function loadSelectedEvent() {
      try {
        setPageLoading(true);
        setError('');

        if (!eventId) {
          throw new Error('Selecione um evento antes de ir para o pagamento.');
        }

        if (!isGuid(eventId)) {
          clearStoredEventId();
          throw new Error('Selecione um evento cadastrado para comprar ingresso.');
        }

        if (!userId) {
          throw new Error('Faca login para comprar ingresso.');
        }

        const selectedEvent = await getEventById(eventId);

        if (!isMounted) {
          return;
        }

        setEvent(selectedEvent);
        setQuantidade(1);
      } catch (requestError) {
        if (isMounted) {
          setError(requestError.message || 'Nao foi possivel carregar o evento selecionado.');
        }
      } finally {
        if (isMounted) {
          setPageLoading(false);
        }
      }
    }

    loadSelectedEvent();

    return () => {
      isMounted = false;
    };
  }, [eventId, userId]);

  const handlePayment = async (e) => {
    e.preventDefault();

    try {
      setLoading(true);
      setError('');

      const createdSale = await createSale({
        userId,
        eventId,
        selectedTicketsUser: quantidade,
      });

      setSale(createdSale);
      setStep('qrcode');
    } catch (requestError) {
      setError(requestError.message || 'Nao foi possivel criar a venda.');
    } finally {
      setLoading(false);
    }
  };

  const handleConfirmPayment = async () => {
    if (!sale?.id) {
      setError('Venda nao encontrada para confirmar o pagamento.');
      setStep('checkout');
      return;
    }

    try {
      setLoading(true);
      setError('');

      const approvedSale = await approveSale(sale.id);
      setSale(approvedSale);

      if (approvedSale?.saleStatus !== 'Approved' || !approvedSale?.ticketId) {
        throw new Error('Pagamento nao aprovado. Tente novamente.');
      }

      setTicketCode(approvedSale.ticketId);
      setStep('confirmed');
    } catch (requestError) {
      setError(requestError.message || 'Nao foi possivel confirmar o pagamento.');
      setStep('checkout');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="page-wrapper">
      <HeaderUser />

      <main className="checkout-container">
        <div className="checkout-card">
          <div className="checkout-icon">
            <span>PIX</span>
          </div>

          <h1 className="checkout-title">Compra de Ingressos</h1>
          <p className="checkout-subtitle">Finalize sua compra e garanta seu ingresso</p>

          {pageLoading ? <p className="payment-status">Carregando evento...</p> : null}
          {error ? <p className="payment-status error">{error}</p> : null}

          {!pageLoading && event ? (
            <div className="selected-event-summary">
              <span className="selected-event-label">Evento selecionado</span>
              <strong>{event.name}</strong>
              <span>{event.date} {event.hour ? `as ${event.hour}` : ''}</span>
            </div>
          ) : null}

          <form onSubmit={handlePayment} className="checkout-form">
            <div className="form-group">
              <label htmlFor="quantidade">Quantidade de Ingressos</label>
              <input
                type="number"
                id="quantidade"
                min="1"
                max={quantidadeMaxima}
                value={quantidade}
                onChange={(e) => {
                  const nextQuantity = parseInt(e.target.value, 10) || 1;
                  setQuantidade(Math.min(quantidadeMaxima, Math.max(1, nextQuantity)));
                }}
                disabled={loading || pageLoading || !event}
              />
            </div>

            <div className="summary-box">
              <div className="summary-row">
                <span>Valor unitario:</span>
                <span className="summary-value">R$ {valorUnitario.toFixed(2).replace('.', ',')}</span>
              </div>
              <div className="summary-row">
                <span>Quantidade:</span>
                <span className="summary-value">{quantidade}</span>
              </div>
              <div className="summary-row total">
                <span>Valor Total:</span>
                <span className="summary-value">R$ {valorTotal.toFixed(2).replace('.', ',')}</span>
              </div>
            </div>

            <div className="form-group">
              <label>Forma de Pagamento</label>
              <div className="payment-option">
                <div className="radio-indicator"></div>

                <div className="pix-icon-box">
                  PIX
                </div>

                <div className="payment-details">
                  <span className="payment-title">PIX</span>
                  <span className="payment-subtitle">Pagamento instantaneo</span>
                </div>
              </div>
            </div>

            <button
              type="submit"
              className="btn-pagamento"
              disabled={loading || pageLoading || !event}
            >
              {loading ? 'Criando venda...' : 'Fazer Pagamento'}
            </button>
          </form>
        </div>
      </main>

      {step === 'qrcode' && (
        <div className="modal-overlay">
          <div className="modal-card">
            <button className="modal-close-btn" onClick={() => setStep('checkout')}>x</button>
            <h2 className="modal-title">Escaneie o QR Code</h2>

            <div className="qr-code-container">
              <img src={qrcodepix} alt="QR Code do PIX" className="qr-code-image" />
              <span className="qr-code-text">QR Code do PIX</span>
            </div>

            <div className="amount-box">
              <span className="amount-label">Valor a pagar:</span>
              <span className="amount-value">R$ {valorTotal.toFixed(2).replace('.', ',')}</span>
            </div>

            <button className="btn-modal-submit" onClick={handleConfirmPayment} disabled={loading}>
              {loading ? 'Confirmando...' : 'Paguei'}
            </button>
          </div>
        </div>
      )}

      {step === 'confirmed' && (
        <div className="modal-overlay">
          <div className="modal-card">
            <button className="modal-close-btn" onClick={() => navigate('/user/home')}>x</button>
            <h2 className="modal-title">Ingresso Confirmado!</h2>

            <div className="success-circle">OK</div>

            <span className="ticket-label">Seu codigo de ingresso:</span>

            <div className="ticket-box">
              <div className="ticket-code">
                {ticketCode}
              </div>
              <span className="ticket-subtext">
                Guarde este codigo para apresentar no evento
              </span>
            </div>

            <div className="email-alert">
              Sua venda foi aprovada e o ticket foi anexado automaticamente.
            </div>

            <button className="btn-modal-submit" onClick={() => navigate('/user/home')}>
              Voltar para Eventos
            </button>
          </div>
        </div>
      )}

      <Footer />
    </div>
  );
}
