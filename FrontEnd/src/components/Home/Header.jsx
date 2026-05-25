import "./Header.css";
import { useNavigate } from "react-router-dom";
import { Ticket, Plus, LogIn, UserPlus } from "lucide-react";
import { canCreateEvent } from "../../utils/auth";

function Header() {
  const navigate = useNavigate();
  const canAccessCreateEvent = canCreateEvent();

  return (
    <header className="header">
      <div className="header-container">
        <div className="logo">
          <Ticket size={18} />
          <span>IngressoJá</span>
        </div>

        <nav className="nav-buttons">
          <button className="active">Eventos</button>

          {canAccessCreateEvent ? (
            <button className="outline" type="button" onClick={() => navigate("/create-event")}>
              <Plus size={16} />
              Criar Evento
            </button>
          ) : null}
        </nav>
      </div>
    </header>
  );
}

export default Header;