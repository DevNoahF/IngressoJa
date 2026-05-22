import "./Header.css";
import { Ticket, Plus, LogIn, UserPlus } from "lucide-react";

function Header() {
  return (
    <header className="header">
      <div className="header-container">
        <div className="logo">
          <Ticket size={18} />
          <span>TicketPro</span>
        </div>

        <nav className="nav-buttons">
          <button className="active">Eventos</button>

          <button className="outline">
            <Plus size={16} />
            Criar Evento
          </button>

          <button className="ghost">
            <LogIn size={16} />
            Login
          </button>

          <button className="dark">
            <UserPlus size={16} />
            Cadastrar
          </button>
        </nav>
      </div>
    </header>
  );
}

export default Header;