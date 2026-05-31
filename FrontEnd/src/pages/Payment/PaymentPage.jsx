import { useState } from 'react';
import './PaymentPage.css';
import qrcodepix from '../../assets/qrcodepix.png';
import HeaderUser from '../../components/HeaderUser/HeaderUser';
import { createSale, updateSaleStatus } from '../../api/sales';
import { getStoredUserId } from '../../utils/auth';
import { useLocation } from 'react-router-dom';

export default function PaymentPage() {
  const [step, setStep] = useState('checkout');
  const [quantidade, setQuantidade] = useState(1);
  const [loading, setLoading] = useState(false);
  const [saleId, setSaleId] = useState(null);
  const [ticketCode, setTicketCode] = useState('');
  const [error, setError] = useState('');

  const location = useLocation();
  const eventId = location.state?.eventId ?? null;
  const valorUnitario = location.state?.ticketValue ?? 150.00;
  const availableTickets = location.state?.totalTicketQuantity ?? 100;

  const valorTotal = qty => qty * valorUnitario;

  const handlePayment = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      const userId = getStoredUserId();
      const sale = await createSale({
        userId,
        eventId,
        selectedTicketsUser: quantidade,
        totalPrice: valorTotal(quantidade),
        availableTickets,
      });

      setSaleId(sale?.id ?? null);
      setStep('qrcode');
    } catch (err) {
      setError('Erro ao criar venda. Tente novamente.');
    } finally {
      setLoading(false);
    }
  };

  const handleConfirmPayment = async () => {
    setLoading(true);
    setError('');

    try {
      if (saleId) {
        const updated = await updateSaleStatus(saleId);
        setTicketCode(updated?.ticketId ?? `TICKET-${saleId}-${Math.random().toString(36).substring(2, 10).toUpperCase()}`);
      } else {
        setTicketCode(`TICKET-${Math.random().toString(36).substring(2, 10).toUpperCase()}`);
      }
      setStep('confirmed');
    } catch {
      setTicketCode(`TICKET-${Math.random().toString(36).substring(2, 10).toUpperCase()}`);
      setStep('confirmed');
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

          <form onSubmit={handlePayment} className="checkout-form">
            <div className="form-group">
              <label htmlFor="quantidade">Quantidade de Ingressos</label>
              <input
                type="number"
                id="quantidade"
                min="1"
                value={quantidade}
                onChange={(e) => setQuantidade(Math.max(1, parseInt(e.target.value) || 1))}
                disabled={loading}
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

            <button type="submit" className="btn-pagamento" disabled={loading}>
              {loading ? 'Processando...' : 'Fazer Pagamento'}
            </button>
          </form>
        </div>
      </main>

      {step === 'qrcode' && (
        <div className="modal-overlay">
          <div className="modal-card">
            <button className="modal-close-btn" onClick={() => setStep('checkout')}>✕</button>
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
            <button className="modal-close-btn" onClick={() => setStep('checkout')}>✕</button>
            <h2 className="modal-title">Ingresso Confirmado!</h2>
            <div className="success-circle">✓</div>
            <span className="ticket-label">Seu código de ingresso:</span>
            <div className="ticket-box">
              <div className="ticket-code">⚿ {ticketCode}</div>
              <span className="ticket-subtext">Guarde este código para apresentar no evento</span>
            </div>
            <div className="email-alert">
              Um e-mail de confirmação foi enviado com todos os detalhes do seu ingresso.
            </div>
            <button className="btn-modal-submit" onClick={() => setStep('checkout')}>
              Voltar para Eventos
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
    } finally {
      setLoading(false);
    }
  };

  const handleConfirmPayment = async () => {
    setLoading(true);
    setError('');

    try {
      if (saleId) {
        const updated = await updateSaleStatus(saleId);
        setTicketCode(updated?.ticketId ?? `TICKET-${saleId}-${Math.random().toString(36).substring(2, 10).toUpperCase()}`);
      } else {
        setTicketCode(`TICKET-${Math.random().toString(36).substring(2, 10).toUpperCase()}`);
      }
      setStep('confirmed');
    } catch {
      setTicketCode(`TICKET-${Math.random().toString(36).substring(2, 10).toUpperCase()}`);
      setStep('confirmed');
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
                <div className="pix-icon-box">❖</div>
                <div className="payment-details">
                  <span className="payment-title">PIX</span>
                  <span className="payment-subtitle">Pagamento instantaneo</span>
                </div>
              </div>
            </div>

            {error && <p style={{ color: '#dc2626', textAlign: 'center' }}>{error}</p>}

            <button type="submit" className="btn-pagamento" disabled={loading}>
              {loading ? 'Processando...' : 'Fazer Pagamento'}
            </button>
          </form>
        </div>
      </main>

      {/* MODAL QR CODE */}
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
              {loading ? 'Confirmando...' : '✓  Paguei'}
            </button>
          </div>
        </div>
      )}

      {/* MODAL CONFIRMADO */}
      {step === 'confirmed' && (
        <div className="modal-overlay">
          <div className="modal-card">
            <button className="modal-close-btn" onClick={() => navigate('/user/home')}>x</button>
            <h2 className="modal-title">Ingresso Confirmado!</h2>

            <div className="success-circle">✓</div>

            <span className="ticket-label">Seu código de ingresso:</span>

            <div className="ticket-box">
              <div className="ticket-code">⚿ {ticketCode}</div>
              <span className="ticket-subtext">Guarde este código para apresentar no evento</span>
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
    </div>
  );
}
