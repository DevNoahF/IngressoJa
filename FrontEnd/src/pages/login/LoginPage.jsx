import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './LoginPage.css';
import { loginAndStoreSession, getStoredRole } from '../../utils/auth';

export default function LoginPage() {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handleLogin = async (e) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    try {
      await loginAndStoreSession({ email, password: senha });
      const role = getStoredRole();
      navigate(role === 'Organizer' ? '/organizer/home' : '/user/home');
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao conectar. Tente novamente.');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-container">
      <div className="login-card">
        {/* Ícone */}
        <div className="login-icon">
          <span>→</span>
        </div>

        {/* Título */}
        <h1 className="login-title">Login</h1>

        {/* Subtítulo */}
        <p className="login-subtitle">Entre com suas credenciais para acessar sua conta</p>

        {/* Formulário */}
        <form onSubmit={handleLogin}>
          {/* Campo de Email */}
          <div className="form-group">
            <label htmlFor="email">Email</label>
            <input
              type="email"
              id="email"
              placeholder="seu@email.com"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              disabled={loading}
            />
          </div>

          {/* Campo de Senha */}
          <div className="form-group">
            <label htmlFor="senha">Senha</label>
            <input
              type="password"
              id="senha"
              placeholder="••••••••"
              value={senha}
              onChange={(e) => setSenha(e.target.value)}
              required
              disabled={loading}
            />
          </div>

          {/* Mensagem de Erro */}
          {error && <div className="error-message">{error}</div>}

          {/* Botão de Login */}
          <button 
            type="submit" 
            className="login-button"
            disabled={loading}
          >
            {loading ? 'Entrando...' : 'Entrar'}
          </button>
        </form>

        {/* Links de Cadastro */}
        <div className="login-footer">
          <p>
            Não tem uma conta?{' '}
            <Link to="/user/register" className="link">
              Cadastre-se como usuário
            </Link>
          </p>
          <p>
            É organizador?{' '}
            <Link to="/organizer/register" className="link">
              Cadastre-se aqui
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
}
