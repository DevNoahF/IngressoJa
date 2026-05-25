import { useEffect, useRef, useState } from "react";
import { ChevronDown, LogOut, Plus, UserCircle2 } from "lucide-react";
import { useNavigate } from "react-router-dom";
import ingressoJaLogo from "../../assets/logo.png";
import "./HeaderOrganizer.css";

export default function HeaderOrganizer() {
	const navigate = useNavigate();
	const [open, setOpen] = useState(false);
	const dropdownRef = useRef(null);

    // IMPLEMENTAR PARA CLICAR NA LOGO E IR DIRETO PARA A PAGINA HOME
	useEffect(() => {
		function handleClickOutside(event) {
			if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
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
		profileImage: "https://i.pravatar.cc/100?img=12", // implementar imagem do backend
	};

	function handleCreateEvent() {
		navigate("/create-event");
	}

	return (
		<header className="header-organizer">
			<div className="header-organizer-container">
				<div className="header-organizer-logo">
					<img
						src={ingressoJaLogo} 
						alt="IngressoJá"
						className="header-organizer-logo-image"
                        onClick={}// implementar para voltar para home
					/>
				</div>

				<div className="header-organizer-actions">
					<button
						type="button"
						className="header-organizer-create-button"
						onClick={}
					>
						<Plus size={16} />
						Criar Evento
					</button>
                    <div className="header-organizer-actions">
                        <button
                            type="button"
                            className="header-organizer-create-button"
                            onClick={}
                        >
                            <Plus size={16} />
                            Ver Vendas
                        </button>


                        <div className="header-organizer-profile" ref={dropdownRef}>
                            <button
                                type="button"
                                onClick={() => setOpen((current) => !current)}
                                className="header-organizer-button"
                            >
                                <img
                                    src={user.profileImage}
                                    alt="Perfil"
                                    className="header-organizer-image"
                                />

                                <span className="header-organizer-name">{user.firstName}</span>

                                <ChevronDown
                                    size={18}
                                    className={`header-organizer-arrow ${open ? "open" : ""}`}
                                />
                            </button>

						{open && (
							<div className="header-organizer-dropdown">
								<button type="button" className="header-organizer-dropdown-item">
									<UserCircle2 size={18} />
									Atualizar dados
								</button>

								<button
									type="button"
									className="header-organizer-dropdown-item header-organizer-dropdown-logout"
								>
									<LogOut size={18} />
									Sair
								</button>
							</div>
						)}
                        </div>
					</div>
				</div>
			</div>
		</header>
	);
}
