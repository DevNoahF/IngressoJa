import { useState } from 'react';
import './PaymentPage.css';
import Header from '../../components/Home/Header';
import Footer from '../../components/Home/Footer';
import qrcodepix from '../../assets/qrcodepix.png'; // Imagem de QR Code para simulação

export default function PaymentPage() {
  // Controle de passos: 'checkout' (tela principal), 'qrcode' (print1), 'confirmed' (print2)
  const [step, setStep] = useState('checkout'); 
  const [quantidade, setQuantidade] = useState(1);
  const [loading, setLoading] = useState(false);
  
  const valorUnitario = 150.00;
  const valorTotal = quantity => quantity * valorUnitario;

  const handlePayment = (e) => {
    e.preventDefault();
    setLoading(true);

    // Simulação rápida para abrir o modal do QR Code
    setTimeout(() => {
      setLoading(false);
      setStep('qrcode');
    }, 600);
  };

  return (
    <div className="page-wrapper">
      <Header />    

      {/* Conteúdo Principal mantém intacto */}
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
              disabled={loading}
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

            <button className="btn-modal-submit" onClick={() => setStep('confirmed')}>
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
                ⚿ TICKET-1-96MZ3LPFI
              </div>
              <span className="ticket-subtext">
                Guarde este código para apresentar no evento
              </span>
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

      <Footer />
    </div>
  );
}