import { useEffect, useMemo, useState } from 'react';
import './PaymentPage.css';
import Footer from '../../components/Home/Footer';
import qrcodepix from '../../assets/qrcodepix.png'; // Imagem de QR Code para simulação
import HeaderUser from '../../components/HeaderUser/HeaderUser';
import { useNavigate } from 'react-router-dom';
import { getEventById } from '../../api/events';
import { getStoredEventId } from '../../utils/eventContext';
import { addPurchasedTicket } from '../../utils/tickets';

const fallbackEvent = {
  id: '',
  name: 'Ingresso selecionado',
  description: 'Finalize o pagamento para registrar seu ingresso.',
  city: 'Cidade não informada',
  state: '',
  street: '',
  neighborhood: '',
  number: '',
  date: '',
  hour: '',
  bannerImage: '',
  ticketValue: 150,
};

export default function PaymentPage() {
  const navigate = useNavigate();
  // Controle de passos: 'checkout' (tela principal), 'qrcode' (print1), 'confirmed' (print2)
  const [step, setStep] = useState('checkout'); 
  const [quantidade, setQuantidade] = useState(1);
  const [loading, setLoading] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState(fallbackEvent);
  const [loadError, setLoadError] = useState('');
  const [pendingTicket, setPendingTicket] = useState(null);

  const currentEventId = getStoredEventId();

  useEffect(() => {
    let isMounted = true;

    async function loadEvent() {
      if (!currentEventId) {
        setSelectedEvent(fallbackEvent);
        setLoadError('Selecione um evento na página inicial antes de pagar.');
        return;
      }

      try {
        const response = await getEventById(currentEventId);

        if (!isMounted) {
          return;
        }

        setSelectedEvent({
          id: response?.id ?? currentEventId,
          name: response?.name ?? fallbackEvent.name,
          description: response?.description ?? fallbackEvent.description,
          city: response?.city ?? fallbackEvent.city,
          state: response?.state ?? fallbackEvent.state,
          street: response?.street ?? fallbackEvent.street,
          neighborhood: response?.neighborhood ?? fallbackEvent.neighborhood,
          number: response?.number ?? fallbackEvent.number,
          date: response?.date ?? fallbackEvent.date,
          hour: response?.hour ?? fallbackEvent.hour,
          bannerImage: response?.bannerImage ?? fallbackEvent.bannerImage,
          ticketValue: Number(response?.ticketValue ?? fallbackEvent.ticketValue),
        });
        setLoadError('');
      } catch (requestError) {
        if (!isMounted) {
          return;
        }

        setSelectedEvent({
          ...fallbackEvent,
          id: currentEventId,
        });
        setLoadError('Não foi possível carregar os detalhes do evento. O pagamento seguirá com os dados disponíveis.');
      }
    }

    loadEvent();

    return () => {
      isMounted = false;
    };
  }, [currentEventId]);

  const valorUnitario = useMemo(() => Number(selectedEvent.ticketValue ?? fallbackEvent.ticketValue), [selectedEvent.ticketValue]);
  const valorTotal = quantity => quantity * valorUnitario;

  const handlePayment = (e) => {
    e.preventDefault();

    if (!currentEventId) {
      return;
    }

    setLoading(true);

    // Simulação rápida para abrir o modal do QR Code
    setTimeout(() => {
      setLoading(false);
      setStep('qrcode');
    }, 600);
  };

  function handleConfirmPayment() {
    const ticket = addPurchasedTicket({
      eventId: selectedEvent.id || currentEventId,
      eventName: selectedEvent.name,
      eventDescription: selectedEvent.description,
      eventCity: selectedEvent.city,
      eventState: selectedEvent.state,
      eventStreet: selectedEvent.street,
      eventNeighborhood: selectedEvent.neighborhood,
      eventNumber: selectedEvent.number,
      eventDate: selectedEvent.date,
      eventHour: selectedEvent.hour,
      bannerImage: selectedEvent.bannerImage,
      quantity,
      unitPrice: valorUnitario,
      totalPrice: valorTotal(quantidade),
    });

    setPendingTicket(ticket);
    setStep('confirmed');
  }

  return (
    <div className="page-wrapper">
      <HeaderUser />    
      {/* Header / Navbar superior */}

      {/* Conteúdo Principal mantém intacto */}
      <main className="checkout-container">
        <div className="checkout-card">
          
          <div className="checkout-icon">
            <span>🛒</span>
          </div>

          <h1 className="checkout-title">Compra de Ingressos</h1>
          <p className="checkout-subtitle">Finalize sua compra e garanta seu ingresso</p>

          {loadError ? <p className="checkout-warning">{loadError}</p> : null}

          <div className="event-summary">
            <div>
              <span className="event-summary-label">Evento selecionado</span>
              <strong>{selectedEvent.name}</strong>
            </div>
            <div>
              <span className="event-summary-label">Valor por ingresso</span>
              <strong>R$ {valorUnitario.toFixed(2).replace('.', ',')}</strong>
            </div>
          </div>

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
                
                <div className="pix-icon-box">
                  ❖
                </div>
                
                <div className="payment-details">
                  <span className="payment-title">PIX</span>
                  <span className="payment-subtitle">Pagamento instantâneo</span>
                </div>
              </div>
            </div>

            <button 
              type="submit" 
              className="btn-pagamento"
              disabled={loading || !currentEventId}
            >
              {loading ? 'Processando...' : 'Fazer Pagamento'}
            </button>
          </form>
        </div>
      </main>

      {/* ==========================================================================
          MODAIS CONTROLADOS PELO ESTADO 'STEP'
         ========================================================================== */}
      
      {/* PASSO 2: Modal do QR Code (Print 1) */}
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

            <button className="btn-modal-submit" onClick={handleConfirmPayment}>
              ✓ &nbsp; Paguei
            </button>
          </div>
        </div>
      )}

      {/* PASSO 3: Modal de Confirmação (Print 2) */}
      {step === 'confirmed' && (
        <div className="modal-overlay">
          <div className="modal-card">
            <button className="modal-close-btn" onClick={() => setStep('checkout')}>✕</button>
            <h2 className="modal-title">Ingresso Confirmado!</h2>
            
            <div className="success-circle">✓</div>

            <span className="ticket-label">Seu código de ingresso:</span>
            
            <div className="ticket-box">
              <div className="ticket-code">
                ⚿ {pendingTicket?.code ?? 'TICKET-EM-BREVE'}
              </div>
              <span className="ticket-subtext">
                Guarde este código para apresentar no evento
              </span>
            </div>

            <div className="email-alert">
              Um e-mail de confirmação foi enviado com todos os detalhes do seu ingresso.
            </div>

            <button className="btn-modal-submit" onClick={() => navigate('/user/tickets')}>
              Ver meus ingressos
            </button>
          </div>
        </div>
      )}

    </div>
  );
}