import { useState } from 'react';
import './PaymentPage.css';
import HeaderUser from '../../components/HeaderUser/HeaderUser';
import  Footer from '../../components/Home/Footer';

export default function PaymentPage() {
  const [quantidade, setQuantidade] = useState(1);
  const [loading, setLoading] = useState(false);
  
  const valorUnitario = 150.00;
  const valorTotal = quantidade * valorUnitario;

  const handlePayment = (e) => {
    e.preventDefault();
    setLoading(true);

    // Simulação da chamada de pagamento
    setTimeout(() => {
      alert('Redirecionando para o pagamento via PIX...');
      setLoading(false);
    }, 1500);
  };

  return (
    <div className="page-wrapper">
      <HeaderUser />    
      {/* Header / Navbar superior */}

      {/* Conteúdo Principal */}
      <main className="checkout-container">
        <div className="checkout-card">
          
          {/* Ícone do Carrinho */}
          <div className="checkout-icon">
            <span>🛒</span>
          </div>

          {/* Títulos */}
          <h1 className="checkout-title">Compra de Ingressos</h1>
          <p className="checkout-subtitle">Finalize sua compra e garanta seu ingresso</p>

          {/* Formulário de Compra */}
          <form onSubmit={handlePayment} className="checkout-form">
            
            {/* Campo de Quantidade */}
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

            {/* Box de Resumo de Valores */}
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
                <span className="summary-value">R$ {valorTotal.toFixed(2).replace('.', ',')}</span>
              </div>
            </div>

            {/* Seção Forma de Pagamento */}
            <div className="form-group">
              <label>Forma de Pagamento</label>
              <div className="payment-option">
                <div className="radio-indicator"></div>
                
                {/* Ícone minimalista simulando o símbolo do PIX */}
                <div className="pix-icon-box">
                  ❖
                </div>
                
                <div className="payment-details">
                  <span className="payment-title">PIX</span>
                  <span className="payment-subtitle">Pagamento instantâneo</span>
                </div>
              </div>
            </div>

            {/* Botão de Envio */}
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

      <Footer />
      
    </div>
  );
}