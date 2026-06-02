import { useEffect, useState } from 'react';
import './PaymentPage.css';
import qrcodepix from '../../assets/qrcodepix.png';
import HeaderUser from '../../components/HeaderUser/HeaderUser';
import { getEventById } from '../../api/events';
import { createSale, updateSaleStatus } from '../../api/sales';
import { getStoredUserId } from '../../utils/auth';
import { getStoredEventId } from '../../utils/eventContext';
import { useLocation, useNavigate } from 'react-router-dom';

export default function PaymentPage() {
  const [step, setStep] = useState('checkout');
  const [quantidade, setQuantidade] = useState(1);
  const [loading, setLoading] = useState(false);
  const [pageLoading, setPageLoading] = useState(true);
  const [pageError, setPageError] = useState('');
  const [event, setEvent] = useState(null);
  const [saleId, setSaleId] = useState(null);
  const [ticketCode, setTicketCode] = useState('');
  const [error, setError] = useState('');

  const location = useLocation();
  const navigate = useNavigate();
  const resolvedEventId = location.state?.eventId ?? getStoredEventId() ?? '';
  const valorUnitario = event?.ticketValue ?? location.state?.ticketValue ?? 150.00;
  const availableTickets = event?.totalTicketQuantity ?? location.state?.totalTicketQuantity ?? 100;

  const valorTotal = qty => qty * valorUnitario;

  useEffect(() => {
    let isMounted = true;

    async function loadEvent() {
      if (!resolvedEventId) {
        if (isMounted) {
          setPageError('Selecione um evento antes de seguir para o pagamento.');
          setPageLoading(false);
        }
        return;
      }

      try {
        setPageLoading(true);
        setPageError('');

        const eventDetails = location.state?.event ?? await getEventById(resolvedEventId);

        if (!isMounted) {
          return;
        }

        setEvent(eventDetails);
      } catch {
        if (isMounted) {
          setPageError('Não foi possível carregar os dados do evento selecionado.');
          setEvent(null);
        }
      } finally {
        if (isMounted) {
          setPageLoading(false);
        }
      }
    }

    loadEvent();

    return () => {
      isMounted = false;
    };
  }, [location.state, resolvedEventId]);

  const handlePayment = async (e) => {
  e.preventDefault();
  setLoading(true);
  setError('');

  const userId = getStoredUserId();

  if (!resolvedEventId) {
    setError('Selecione um evento válido para finalizar a compra.');
    setLoading(false);
    return;
  }

  try {
    const sale = await createSale({
      userId,
      eventId: resolvedEventId,
      selectedTicketsUser: quantidade,
    });

    console.log('CONTEÚDO REAL DO BACK-END:', sale);

    setSaleId(sale?.id ?? sale?.Id ?? null);
    setStep('qrcode');
  } catch (err) {
    setError(err?.message?.includes('400') ? 'Não foi possível criar a venda. Verifique o evento selecionado e tente novamente.' : 'Erro ao criar venda. Tente novamente.');
  } finally {
    setLoading(false);
  }
};

const handleConfirmPayment = async () => {
  setLoading(true);
  setError('');

  try {
    console.log('Confirmando pagamento para saleId:', saleId);
    
    if (saleId !== null && saleId !== undefined) {
      const updatedSale = await updateSaleStatus(saleId);
      
      console.log('RESPOSTA DO PATCH (CONFIRMAÇÃO):', updatedSale);

      const ticket = updatedSale?.ticketId ?? updatedSale?.TicketId ?? updatedSale?.ticketCode ?? '';
      
      setTicketCode(ticket);
      
      setStep('confirmed');
      return;
    }

    throw new Error('Sale id not available.');
  } catch (confirmError) {
    console.error('Erro detalhado ao confirmar pagamento:', confirmError);
    setError('Não foi possível aprovar a venda.');
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
            <span>🛒</span>
          </div>

          <h1 className="checkout-title">Compra de Ingressos</h1>
          <p className="checkout-subtitle">Finalize sua compra e garanta seu ingresso</p>

          {pageLoading ? <p className="payment-status">Carregando evento...</p> : null}
          {pageError ? <p className="payment-status error">{pageError}</p> : null}

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
                value={quantidade}
                onChange={(e) => setQuantidade(Math.max(1, parseInt(e.target.value) || 1))}
                disabled={loading || pageLoading || !event}
              />
            </div>

            <div className="summary-box">
              <div className="summary-row">
                <span>Valor unitário:</span>
                <span className="summary-value">R$ {valorUnitario.toFixed(2).replace('.', ',')}</span>
              </div>
              <div className="summary-row">
                <span>Quantidade:</span>
                <span className="summary-value">{quantidade}</span>
              </div>
              <div className="summary-row total">
                <span>Valor Total:</span>
                <span className="summary-value">R$ {valorTotal(quantidade).toFixed(2).replace('.', ',')}</span>
              </div>
            </div>

            <div className="form-group">
              <label>Forma de Pagamento</label>
              <div className="payment-option">
                <div className="radio-indicator"></div>
                <div className="pix-icon-box">❖</div>
                <div className="payment-details">
                  <span className="payment-title">PIX</span>
                  <span className="payment-subtitle">Pagamento instantâneo</span>
                </div>
              </div>
            </div>

            {error && <p style={{ color: '#dc2626', textAlign: 'center' }}>{error}</p>}

            <button type="submit" className="btn-pagamento" disabled={loading || pageLoading || !event}>
              {loading ? 'Processando...' : 'Fazer Pagamento'}
            </button>
          </form>
        </div>
      </main>

      {step === 'qrcode' && (
        <div className="modal-overlay">
          <div className="modal-card">
            <button type="button" className="modal-close-btn" onClick={() => setStep('checkout')}>✕</button>
            <h2 className="modal-title">Escaneie o QR Code</h2>
            <div className="qr-code-container">
              <img src={qrcodepix} alt="QR Code do PIX" className="qr-code-image" />
              <span className="qr-code-text">QR Code do PIX</span>
            </div>
            <div className="amount-box">
              <span className="amount-label">Valor a pagar:</span>
              <span className="amount-value">R$ {valorTotal(quantidade).toFixed(2).replace('.', ',')}</span>
            </div>
            <button className="btn-modal-submit" onClick={handleConfirmPayment} disabled={loading}>
              {loading ? 'Confirmando...' : '✓  Paguei'}
            </button>
          </div>
        </div>
      )}

      {step === 'confirmed' && (
        <div className="modal-overlay">
          <div className="modal-card">
            <button type="button" className="modal-close-btn" onClick={() => setStep('checkout')}>✕</button>
            <h2 className="modal-title">Ingresso Confirmado!</h2>
            <div className="success-circle">✓</div>
            <span className="ticket-label">Venda aprovada com sucesso.</span>
            <div className="ticket-box">
              <div className="ticket-code">{ticketCode ? `⚿ ${ticketCode}` : 'Ticket será gerado pelo back'}</div>
              <span className="ticket-subtext">A confirmação da venda foi concluída no servidor.</span>
            </div>
            <div className="email-alert">
              Um e-mail de confirmação foi enviado com todos os detalhes do seu ingresso.
            </div>
            <button type="button" className="btn-modal-submit" onClick={() => navigate('/user/home')}>
              Voltar para Eventos
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
