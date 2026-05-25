import { useState, useRef, useEffect } from "react";
import { ChevronDown, LogOut, UserCircle2 } from "lucide-react";
import ingressoJaLogo from "../../assets/logo.png";
import "./HeaderUser.css";
import { useNavigate } from "react-router-dom";

export default function HeaderUser() {
  const [open, setOpen] = useState(false);
  const dropdownRef = useRef(null);
  const navigate = useNavigate();

  // Fecha o dropdown ao clicar fora
  useEffect(() => {
    function handleClickOutside(event) {
      if (
        dropdownRef.current &&
        !dropdownRef.current.contains(event.target)
      ) {
        setOpen(false);
      }
    }

    document.addEventListener("mousedown", handleClickOutside);

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  const user = {
    firstName: "Noah", // implementar firstname do backend
    profileImage:
      "https://i.pravatar.cc/100?img=12", // implementar imagem do backend
  };

  return (
    <header className="header-user">
      <div className="header-user-container">
        <div className="header-user-logo">
          <img
            src={ingressoJaLogo}
            alt="IngressoJá"
            className="header-user-logo-image"
            onClick={() => navigate("/home")} // Implementar para voltar para home
          />
        </div>

        <div className="header-user-profile" ref={dropdownRef}>
          <button
            onClick={() => setOpen(!open)}
            className="header-user-button"
          >
            <img
              src={user.profileImage}
              alt="Perfil"
              className="header-user-image"
            />

            <span className="header-user-name">{user.firstName}</span>

            <ChevronDown
              size={18}
              className={`header-user-arrow ${open ? "open" : ""}`}
            />
          </button>

          {open && (
            <div className="header-user-dropdown">
              <button className="header-user-dropdown-item">
                <UserCircle2 size={18} />
                Atualizar dados
              </button>

              <button className="header-user-dropdown-item header-user-dropdown-logout">
                <LogOut size={18} />
                Sair
              </button>
            </div>
          )}
        </div>
      </div>
    </header>
  );
}