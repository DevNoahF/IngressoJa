import "./OrganizerEvents.css";
import { useEffect, useMemo, useState } from "react";
import { MapPin, Calendar, Clock, Ticket } from "lucide-react";
import HeaderOrganizer from "../../components/headerOrganizer/HeaderOrganizer";
import OrganizerEventCard from "../../components/OrganizerEvents/OrganizerEventCard";
import { getEventsByOrganizerId, getStateCode } from "../../api/events";
import { getStoredUserId } from "../../utils/auth";

const fallbackImage = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f";

const mockEvents = [
  {
    id: "mock-1",
    name: "Festival de Verão",
    description: "Evento mockado para teste da rota do organizador.",
    location: "São Paulo - SP",
    formattedDate: "15/07/2026",
    hour: "18:00",
    bannerImage: fallbackImage,
    totalTicketQuantity: 120,
    status: "upcoming",
  },
  {
    id: "mock-2",
    name: "Noite Eletrônica",
    description: "Evento mockado para teste da rota do organizador.",
    location: "Rio de Janeiro - RJ",
    formattedDate: "22/07/2026",
    hour: "20:30",
    bannerImage: fallbackImage,
    totalTicketQuantity: 90,
    status: "upcoming",
  },
  {
    id: "mock-3",
    name: "Samba Sunset",
    description: "Evento mockado para teste da rota do organizador.",
    location: "Belo Horizonte - MG",
    formattedDate: "28/07/2026",
    hour: "17:00",
    bannerImage: fallbackImage,
    totalTicketQuantity: 150,
    status: "finished",
  },
  {
    id: "mock-4",
    name: "Tech Conference",
    description: "Evento mockado para teste da rota do organizador.",
    location: "Curitiba - PR",
    formattedDate: "02/08/2026",
    hour: "09:00",
    bannerImage: fallbackImage,
    totalTicketQuantity: 300,
    status: "upcoming",
  },
];

function normalizeEvent(event) {
  const city = event.city ?? "";
  const stateCode = getStateCode(event.state);

  return {
    id: event.id,
    name: event.name ?? "Evento sem nome",
    description: event.description ?? "",
    location: stateCode ? `${city} - ${stateCode}` : city,
    formattedDate: event.date ?? "Data não informada",
    hour: event.hour ?? "--:--",
    bannerImage: event.bannerImage || fallbackImage,
    totalTicketQuantity: event.totalTicketQuantity ?? 0,
    ticketValue: event.ticketValue ?? 0,
    status: event.status ?? "",
  };
}

function OrganizerEvents() {
  const organizerId = getStoredUserId().trim();
  const [events, setEvents] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");

  // ESTADOS PARA OS MODAIS
  const [showEditModal, setShowEditModal] = useState(false);
  const [showRevenueModal, setShowRevenueModal] = useState(false);
  const [showDescriptionModal, setShowDescriptionModal] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState(null);

  // Funções para abrir modais
  const handleEditClick = (event) => {
    setSelectedEvent(event);
    setShowEditModal(true);
  };

  const handleRevenueClick = (event) => {
    setSelectedEvent(event);
    setShowRevenueModal(true);
  };

  const handlePhotoClick = (event) => {
    setSelectedEvent(event);
    setShowDescriptionModal(true);
  };

  const handleCloseModals = () => {
    setShowEditModal(false);
    setShowRevenueModal(false);
    setShowDescriptionModal(false);
    setSelectedEvent(null);
  };

  useEffect(() => {
    let isMounted = true;

    async function loadEvents() {
      if (!organizerId) {
        setEvents(mockEvents);
        setError("");
        setIsLoading(false);
        return;
      }

      try {
        setIsLoading(true);
        setError("");

        const response = await getEventsByOrganizerId(organizerId);

        if (!isMounted) {
          return;
        }

        setEvents(Array.isArray(response) ? response.map(normalizeEvent) : []);
      } catch (requestError) {
        if (isMounted) {
          setError("");
          setEvents(mockEvents);
        }
      } finally {
        if (isMounted) {
          setIsLoading(false);
        }
      }
    }

    loadEvents();

    return () => {
      isMounted = false;
    };
  }, [organizerId]);

  const visibleEvents = useMemo(() => {
    return events.slice(0, 4);
  }, [events]);

  return (
    <div className="organizer-events-page">
      <HeaderOrganizer />

      <main className="organizer-events-main">
        <section className="organizer-events-grid" aria-live="polite">
          {isLoading ? <p className="organizer-events-state">Carregando eventos...</p> : null}
          {error ? <p className="organizer-events-state error">{error}</p> : null}

          {!isLoading && !error && visibleEvents.length === 0 ? (
            <p className="organizer-events-state">Nenhum evento encontrado.</p>
          ) : null}

          {visibleEvents.map((event) => (
            <OrganizerEventCard
                key={event.id}
                event={event}
                onEdit={() => handleEditClick(event)}
                onRevenue={() => handleRevenueClick(event)}
                onPhotoClick={() => handlePhotoClick(event)}
            />
          ))}
        </section>
      </main>

      {/* MODAL 1: EDITAR EVENTO */}
      {showEditModal && selectedEvent && (
        <div className="organizer-modal-overlay">
          <div className="organizer-modal-card">
            <div className="organizer-modal-header">
              <h2 className="organizer-modal-title">Editar Evento</h2>
              <button type="button" className="organizer-modal-close-btn" onClick={handleCloseModals}>
                ✕
              </button>
            </div>

            <div className="organizer-modal-body-scroll">
              <form className="edit-event-form">
                <div className="form-group full-width">
                  <label>NOME DO EVENTO</label>
                  <input type="text" defaultValue={selectedEvent.name} />
                </div>

                <div className="form-group full-width">
                  <label>DESCRIÇÃO</label>
                  <textarea defaultValue={selectedEvent.description} />
                </div>

                <div className="form-group">
                  <label>RUA (STREET)</label>
                  <input type="text" placeholder="Ex: Rua das Flores" />
                </div>

                <div className="form-group">
                  <label>NÚMERO</label>
                  <input type="text" placeholder="Ex: 123" />
                </div>

                <div className="form-group">
                  <label>BAIRRO (NEIGHBORHOOD)</label>
                  <input type="text" />
                </div>

                <div className="form-group">
                  <label>CIDADE (CITY)</label>
                  <input type="text" defaultValue={selectedEvent.location.split(" - ")[0]} />
                </div>

                <div className="form-group">
                  <label>ESTADO (STATE)</label>
                  <select>
                    <option value="SP">São Paulo</option>
                    <option value="RJ">Rio de Janeiro</option>
                    <option value="MG">Minas Gerais</option>
                    {/* Adicionar outros conforme seu ENUM */}
                  </select>
                </div>

                <div className="form-group">
                  <label>DATA E HORA (COMPLETO)</label>
                  <input type="datetime-local" />
                </div>

                <div className="form-group">
                  <label>DATA (DATE ONLY)</label>
                  <input type="date" defaultValue={selectedEvent.date} />
                </div>

                <div className="form-group">
                  <label>HORA (TIME ONLY)</label>
                  <input type="time" defaultValue={selectedEvent.hour} />
                </div>

                <div className="form-group">
                  <label>TOTAL DE TICKETS</label>
                  <input type="number" defaultValue={selectedEvent.totalTicketQuantity} />
                </div>

                <div className="form-group full-width">
                  <label>STATUS DO EVENTO</label>
                  <select defaultValue={selectedEvent.status}>
                    <option value="upcoming">Próximo</option>
                    <option value="finished">Encerrado</option>
                  </select>
                </div>
              </form>
            </div>

            <button type="button" className="organizer-modal-save-btn" onClick={handleCloseModals}>
              Salvar Alterações
            </button>
          </div>
        </div>
      )}

      {/* MODAL 2: VER RECEITA */}
      {showRevenueModal && selectedEvent && (
        <div className="organizer-modal-overlay">
          <div className="organizer-modal-card">
            <div className="organizer-modal-header">
              <h2 className="organizer-modal-title">Relatório de Vendas</h2>
              <button type="button" className="organizer-modal-close-btn" onClick={handleCloseModals}>
                ✕
              </button>
            </div>

            <div className="revenue-stats">
              <h3 style={{textAlign: "center", marginBottom: "10px"}}>{selectedEvent.name}</h3>
              
              <div className="stat-item">
                <span className="stat-label">Ingressos Vendidos</span>
                <span className="stat-value">124 / {selectedEvent.totalTicketQuantity || 0}</span>
              </div>

              <div className="stat-item">
                <span className="stat-label">Receita Total Bruta</span>
                <span className="stat-value">R$ 18.600,00</span>
              </div>
            </div>

            <button type="button" className="organizer-modal-save-btn" onClick={handleCloseModals}>
              Fechar
            </button>
          </div>
        </div>
      )}

      {/* MODAL 3: VER DESCRIÇÃO DO EVENTO */}
      {showDescriptionModal && selectedEvent && (
        <div className="organizer-modal-overlay">
          <div className="organizer-modal-card">
            <div className="organizer-modal-header">
              <h2 className="organizer-modal-title">Descrição do Evento</h2>
              <button type="button" className="organizer-modal-close-btn" onClick={handleCloseModals}>
                ✕
              </button>
            </div>

            <div className="organizer-description-modal-body">
              <img 
                src={selectedEvent.bannerImage} 
                alt={selectedEvent.name}
                className="organizer-description-modal-image"
              />
              
              <h3 className="organizer-description-modal-title">{selectedEvent.name}</h3>
              
              <p className="organizer-description-modal-text">
                {selectedEvent.description || "Nenhuma descrição disponível."}
              </p>

              <div className="organizer-description-modal-details">
                <span>
                  <MapPin size={16} />
                  {selectedEvent.location}
                </span>
                <span>
                  <Calendar size={16} />
                  {selectedEvent.formattedDate}
                </span>
                <span>
                  <Clock size={16} />
                  {selectedEvent.hour}
                </span>
                <span>
                  <Ticket size={16} />
                  {selectedEvent.totalTicketQuantity} Total de ingressos
                </span>
                <span>
                  <Ticket size={16} />
                  R$ {Number(selectedEvent.ticketValue).toFixed(2)} por ingresso
                </span>
              </div>
            </div>

            <button type="button" className="organizer-modal-save-btn" onClick={handleCloseModals}>
              Fechar
            </button>
          </div>
        </div>
      )}
    </div>
  );
}

export default OrganizerEvents;