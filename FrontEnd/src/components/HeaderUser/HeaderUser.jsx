import { useState, useRef, useEffect } from "react";
import { ChevronDown, LogOut, Ticket, UserCircle2 } from "lucide-react";
import ingressoJaLogo from "../../assets/logo.png";
import "./HeaderUser.css";
import { useNavigate } from "react-router-dom";
import { getStoredUserId, clearAuthSession } from "../../utils/auth";
import { getUser } from "../../api/users";

export default function HeaderUser() {
  const [open, setOpen] = useState(false);
  const dropdownRef = useRef(null);
  const navigate = useNavigate();
  const [userData, setUserData] = useState({
    firstName: "Noah",
    profileImage: "https://i.pravatar.cc/100?img=12",
  });

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

  // Carrega dados dinâmicos do usuário logado
  useEffect(() => {
    const userId = getStoredUserId();
    if (userId) {
      getUser(userId)
        .then((data) => {
          if (data) {
            setUserData({
              firstName: data.firstName || "Usuário",
              profileImage: data.photoProfile?.value || data.photoProfile || "https://i.pravatar.cc/100?img=12",
            });
          }
        })
        .catch((err) => console.error("Erro ao carregar dados do usuário:", err));
    }
  }, []);

  return (
    <header className="header-user">
      <div className="header-user-container">
        <div className="header-user-logo">
          <img
            src={ingressoJaLogo}
            alt="IngressoJá"
            className="header-user-logo-image"
            onClick={() => navigate("/user/home")}
          />
        </div>

        <div className="header-user-profile" ref={dropdownRef}>
          <button
            type="button"
            onClick={() => setOpen((v) => !v)}
            className="header-user-button"
          >
            <img
              src={userData.profileImage}
              alt="Perfil"
              className="header-user-image"
            />

            <span className="header-user-name">{userData.firstName}</span>

            <ChevronDown
              size={18}
              className={`header-user-arrow ${open ? "open" : ""}`}
            />
          </button>

          {open && (
            <div className="header-user-dropdown">
              <button
                type="button"
                onClick={() => {
                  setOpen(false);
                  try { navigate("/update"); } catch(e) { console.error(e); }
                }}
                className="header-user-dropdown-item"
              >
                <UserCircle2 size={18} />
                Atualizar dados
              </button>

              <button
                type="button"
                onClick={() => {
                  setOpen(false);
                  try { navigate('/user/purchases'); } catch(e) { console.error(e); }
                }}
                className="header-user-dropdown-item"
              >
                <Ticket size={18} />
                Minhas compras
              </button>

              <button
                type="button"
                onClick={() => {
                  clearAuthSession();
                  try { navigate("/login"); } catch(e) { console.error(e); }
                }}
                className="header-user-dropdown-item header-user-dropdown-logout"
              >
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