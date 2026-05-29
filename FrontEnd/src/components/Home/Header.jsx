import "./Header.css";
import { useNavigate } from "react-router-dom";
import { Ticket, Plus, LogIn, UserPlus } from "lucide-react";
function Header() {
  const navigate = useNavigate();

  return (
    <header className="header">
      <div className="header-container">
        <div className="logo">
          <Ticket size={18} />
          <span>IngressoJá</span>
        </div>

        <nav className="nav-buttons">
          <button className="active">Eventos</button>

          <button className="outline" type="button" onClick={() => navigate("/organizer/create")}>
            <Plus size={16} />
            Criar Evento
          </button>
        </nav>
      </div>
    </header>
  );
}

export default Header;