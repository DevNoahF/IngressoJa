import "./OrganizerEvents.css";
import { useEffect, useMemo, useState } from "react";
import { MapPin, Calendar, Clock, Ticket } from "lucide-react";
import HeaderOrganizer from "../../components/headerOrganizer/HeaderOrganizer";
import OrganizerEventCard from "../../components/OrganizerEvents/OrganizerEventCard";
import { getEventsByOrganizerId, getEventById, getStateCode, statesOptions, updateEvent, deleteEvent } from "../../api/events";
import { getStoredUserId } from "../../utils/auth";

const fallbackImage = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f";

const PERMANENT_EVENT = {
  id: "permanent-semanca-1",
  name: "Show da SeManca e SeMata",
  description: "Uma noite inesquecível onde a SeManca toca trompete com os pés e a SeMata ensina passos de dança proibidos até pela física. Riso garantido ou SeMata direto para sua casa.",
  location: "Praça do Caos Glorioso - ZZ",
  formattedDate: "31/12/2026",
  hour: "23:59",
  bannerImage: "https://i.pinimg.com/736x/c5/53/79/c55379996a160a72d08150c3b05db17d.jpg",
  totalTicketQuantity: 420,
  ticketValue: 99.9,
  status: "upcoming",
};

function normalizeEvent(event) {
  const city = event.city ?? "";
  const stateCode = getStateCode(event.state);
  return {
    id: event.id,
    name: event.name ?? "Evento sem nome",
    description: event.description ?? "",
    city,
    state: event.state ?? 0,
    street: event.street ?? "",
    number: event.number ?? 0,
    neighborhood: event.neighborhood ?? "",
    location: stateCode ? `${city} - ${stateCode}` : city,
    formattedDate: event.date ?? "Data não informada",
    date: event.date ?? "",
    hour: event.hour ?? "--:--",
    bannerImage: event.bannerImage || fallbackImage,
    totalTicketQuantity: event.totalTicketQuantity ?? 0,
    ticketValue: event.ticketValue ?? 0,
    status: event.status ?? "",
  };
}

function toDateInputValue(dateValue) {
  if (!dateValue) return "";
  if (/^\d{4}-\d{2}-\d{2}$/.test(dateValue)) return dateValue;
  if (/^\d{2}\/\d{2}\/\d{4}$/.test(dateValue)) {
    const [day, month, year] = dateValue.split("/");
    return `${year}-${month}-${day}`;
  }
  return "";
}

function toStateValue(state) {
  return state ? String(state) : "";
}

function buildEditForm(event) {
  return {
    name: event.name ?? "",
    description: event.description ?? "",
    street: event.street ?? "",
    number: String(event.number ?? ""),
    neighborhood: event.neighborhood ?? "",
    city: event.city ?? "",
    state: toStateValue(event.state),
    date: toDateInputValue(event.date ?? event.formattedDate ?? ""),
    hour: event.hour ?? "",
    ticketValue: String(event.ticketValue ?? ""),
    totalTicketQuantity: String(event.totalTicketQuantity ?? ""),
    bannerImage: event.bannerImage ?? "",
  };
}

function OrganizerEvents() {
  const organizerId = getStoredUserId().trim();
  const [events, setEvents] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");
  const [isDetailLoading, setIsDetailLoading] = useState(false);

  const [showEditModal, setShowEditModal] = useState(false);
  const [showRevenueModal, setShowRevenueModal] = useState(false);
  const [showDescriptionModal, setShowDescriptionModal] = useState(false);
  const [showStatusModal, setShowStatusModal] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState(null);
  const [editForm, setEditForm] = useState(null);
  const [editFeedback, setEditFeedback] = useState({ type: "", message: "" });
  const [isSavingEdit, setIsSavingEdit] = useState(false);
  const [selectedStatus, setSelectedStatus] = useState(null);
  const [statusFeedback, setStatusFeedback] = useState({ type: "", message: "" });
  const [isSavingStatus, setIsSavingStatus] = useState(false);

  const handleEditClick = (event) => {
    setSelectedEvent(event);
    setEditForm(buildEditForm(event));
    setEditFeedback({ type: "", message: "" });
    setShowEditModal(true);
  };

  const handleRevenueClick = (event) => {
    setSelectedEvent(event);
    setShowRevenueModal(true);
  };

  const handlePhotoClick = async (event) => {
    if (event.id === PERMANENT_EVENT.id) {
      setSelectedEvent(event);
      setShowDescriptionModal(true);
      return;
    }

    setSelectedEvent(event);
    setShowDescriptionModal(true);
    setIsDetailLoading(true);

    try {
      const fullEvent = await getEventById(event.id);
      setSelectedEvent(normalizeEvent(fullEvent));
    } catch {
      // mantém o evento parcial se falhar
    } finally {
      setIsDetailLoading(false);
    }
  };

  const handleStatusClick = (event) => {
    setSelectedEvent(event);
    setSelectedStatus(null);
    setStatusFeedback({ type: "", message: "" });
    setShowStatusModal(true);
  };

  const handleDeleteClick = async (eventToDelete) => {
    if (!eventToDelete) return;
    const confirmed = window.confirm(`Excluir o evento "${eventToDelete.name}"? Esta ação é irreversível.`);
    if (!confirmed) return;
    try {
      await deleteEvent(eventToDelete.id);
      setEvents((current) => current.filter((e) => e.id !== eventToDelete.id));
    } catch (err) {
      setError(err instanceof Error ? err.message : "Não foi possível excluir o evento.");
    }
  };

  const handleCloseModals = () => {
    setShowEditModal(false);
    setShowRevenueModal(false);
    setShowDescriptionModal(false);
    setShowStatusModal(false);
    setSelectedEvent(null);
    setEditForm(null);
    setEditFeedback({ type: "", message: "" });
    setIsSavingEdit(false);
    setSelectedStatus(null);
    setStatusFeedback({ type: "", message: "" });
    setIsSavingStatus(false);
    setIsDetailLoading(false);
  };

  function handleEditFormChange(event) {
    const { name, value } = event.target;
    setEditForm((currentForm) => ({ ...currentForm, [name]: value }));
  }

  async function handleEditSubmit(event) {
    event.preventDefault();
    if (!selectedEvent || !editForm) return;
    setIsSavingEdit(true);
    setEditFeedback({ type: "", message: "" });
    try {
      const updatedEvent = await updateEvent(selectedEvent.id, {
        name: editForm.name,
        description: editForm.description,
        street: editForm.street,
        neighborhood: editForm.neighborhood,
        city: editForm.city,
        number: Number(editForm.number),
        state: Number(editForm.state),
        date: editForm.date,
        hour: editForm.hour,
        ticketValue: Number(editForm.ticketValue),
        totalTicketQuantity: Number(editForm.totalTicketQuantity),
        bannerImage: editForm.bannerImage,
      });
      setEvents((currentEvents) =>
        currentEvents.map((currentEvent) =>
          currentEvent.id === selectedEvent.id ? updatedEvent : currentEvent
        )
      );
      setSelectedEvent(updatedEvent);
      setEditForm(buildEditForm(updatedEvent));
      setEditFeedback({ type: "success", message: "Evento atualizado com sucesso." });
    } catch (requestError) {
      setEditFeedback({
        type: "error",
        message: requestError instanceof Error ? requestError.message : "Não foi possível atualizar o evento.",
      });
    } finally {
      setIsSavingEdit(false);
    }
  }

  const handleStatusSubmit = async () => {
    if (!selectedEvent || selectedStatus === null) return;
    setIsSavingStatus(true);
    try {
      await fetch(`http://localhost:5000/events/${selectedEvent.id}`, {
        method: 'PATCH',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ status: selectedStatus }),
      });
      setStatusFeedback({ type: 'success', message: 'Status atualizado com sucesso!' });
    } catch {
      setStatusFeedback({ type: 'error', message: 'Erro ao atualizar status.' });
    } finally {
      setIsSavingStatus(false);
    }
  };

  useEffect(() => {
    let isMounted = true;
    async function loadEvents() {
      if (!organizerId) {
        setEvents([]);
        setError("");
        setIsLoading(false);
        return;
      }
      try {
        setIsLoading(true);
        setError("");
        const response = await getEventsByOrganizerId(organizerId);
        if (!isMounted) return;
        setEvents(Array.isArray(response) ? response.map(normalizeEvent) : []);
      } catch (requestError) {
        if (isMounted) {
          setError("");
          setEvents([]);
        }
      } finally {
        if (isMounted) setIsLoading(false);
      }
    }
    loadEvents();
    return () => { isMounted = false; };
  }, [organizerId]);

  const visibleEvents = useMemo(() => events.slice(0, 4), [events]);

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

          <OrganizerEventCard
            key={PERMANENT_EVENT.id}
            event={PERMANENT_EVENT}
            onEdit={() => handleEditClick(PERMANENT_EVENT)}
            onRevenue={() => handleRevenueClick(PERMANENT_EVENT)}
            onPhotoClick={() => handlePhotoClick(PERMANENT_EVENT)}
            onStatus={() => handleStatusClick(PERMANENT_EVENT)}
          />

          {visibleEvents.map((event) => (
            <OrganizerEventCard
              key={event.id}
              event={event}
              onEdit={() => handleEditClick(event)}
              onRevenue={() => handleRevenueClick(event)}
              onPhotoClick={() => handlePhotoClick(event)}
              onDelete={() => handleDeleteClick(event)}
              onStatus={() => handleStatusClick(event)}
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
              <button type="button" className="organizer-modal-close-btn" onClick={handleCloseModals}>✕</button>
            </div>
            <div className="organizer-modal-body-scroll">
              <form id="edit-event-form" className="edit-event-form" onSubmit={handleEditSubmit}>
                <div className="form-group full-width">
                  <label>NOME DO EVENTO</label>
                  <input type="text" name="name" value={editForm?.name ?? ""} onChange={handleEditFormChange} required />
                </div>
                <div className="form-group full-width">
                  <label>DESCRIÇÃO</label>
                  <textarea name="description" value={editForm?.description ?? ""} onChange={handleEditFormChange} required />
                </div>
                <div className="form-group">
                  <label>RUA (STREET)</label>
                  <input type="text" name="street" value={editForm?.street ?? ""} onChange={handleEditFormChange} required />
                </div>
                <div className="form-group">
                  <label>NÚMERO</label>
                  <input type="number" min="0" name="number" value={editForm?.number ?? ""} onChange={handleEditFormChange} required />
                </div>
                <div className="form-group">
                  <label>BAIRRO (NEIGHBORHOOD)</label>
                  <input type="text" name="neighborhood" value={editForm?.neighborhood ?? ""} onChange={handleEditFormChange} required />
                </div>
                <div className="form-group">
                  <label>CIDADE (CITY)</label>
                  <input type="text" name="city" value={editForm?.city ?? ""} onChange={handleEditFormChange} required />
                </div>
                <div className="form-group">
                  <label>ESTADO (STATE)</label>
                  <select name="state" value={editForm?.state ?? ""} onChange={handleEditFormChange} required>
                    <option value="">Selecione o estado</option>
                    {statesOptions.map((state) => (
                      <option key={state.value} value={state.value}>{state.code} - {state.name}</option>
                    ))}
                  </select>
                </div>
                <div className="form-group">
                  <label>DATA</label>
                  <input type="date" name="date" value={editForm?.date ?? ""} onChange={handleEditFormChange} required />
                </div>
                <div className="form-group">
                  <label>HORA</label>
                  <input type="time" name="hour" value={editForm?.hour ?? ""} onChange={handleEditFormChange} required />
                </div>
                <div className="form-group">
                  <label>TOTAL DE TICKETS</label>
                  <input type="number" min="1" name="totalTicketQuantity" value={editForm?.totalTicketQuantity ?? ""} onChange={handleEditFormChange} required />
                </div>
                <div className="form-group full-width">
                  <label>VALOR DO INGRESSO</label>
                  <input type="number" min="0" step="0.01" name="ticketValue" value={editForm?.ticketValue ?? ""} onChange={handleEditFormChange} required />
                </div>
                <div className="form-group full-width">
                  <label>BANNER DO EVENTO</label>
                  <input type="url" name="bannerImage" value={editForm?.bannerImage ?? ""} onChange={handleEditFormChange} required />
                </div>
                {editFeedback.message ? (
                  <p className={`form-feedback ${editFeedback.type}`}>{editFeedback.message}</p>
                ) : null}
              </form>
            </div>
            <button type="submit" form="edit-event-form" className="organizer-modal-save-btn" disabled={isSavingEdit}>
              {isSavingEdit ? "Salvando..." : "Salvar Alterações"}
            </button>
          </div>
        </div>
      )}


      {showRevenueModal && selectedEvent && (
        <div className="organizer-modal-overlay">
          <div className="organizer-modal-card">
            <div className="organizer-modal-header">
              <h2 className="organizer-modal-title">Relatório de Vendas</h2>
              <button type="button" className="organizer-modal-close-btn" onClick={handleCloseModals}>✕</button>
            </div>
            <div className="revenue-stats">
              <h3 style={{ textAlign: "center", marginBottom: "10px" }}>{selectedEvent.name}</h3>
              <div className="stat-item">
                <span className="stat-label">Ingressos Vendidos</span>
                <span className="stat-value">124 / {selectedEvent.totalTicketQuantity || 0}</span>
              </div>
              <div className="stat-item">
                <span className="stat-label">Receita Total Bruta</span>
                <span className="stat-value">R$ 18.600,00</span>
              </div>
            </div>
            <button type="button" className="organizer-modal-save-btn" onClick={handleCloseModals}>Fechar</button>
          </div>
        </div>
      )}


      {showDescriptionModal && selectedEvent && (
        <div className="organizer-modal-overlay">
          <div className="organizer-modal-card">
            <div className="organizer-modal-header">
              <h2 className="organizer-modal-title">Descrição do Evento</h2>
              <button type="button" className="organizer-modal-close-btn" onClick={handleCloseModals}>✕</button>
            </div>
            <div className="organizer-description-modal-body">
              {isDetailLoading ? (
                <p style={{ textAlign: "center" }}>Carregando detalhes...</p>
              ) : (
                <>
                  <img src={selectedEvent.bannerImage} alt={selectedEvent.name} className="organizer-description-modal-image" />
                  <h3 className="organizer-description-modal-title">{selectedEvent.name}</h3>
                  <p className="organizer-description-modal-text">{selectedEvent.description || "Nenhuma descrição disponível."}</p>
                  <div className="organizer-description-modal-details">
                    <span><MapPin size={16} />{selectedEvent.location}</span>
                    <span><Calendar size={16} />{selectedEvent.formattedDate}</span>
                    <span><Clock size={16} />{selectedEvent.hour}</span>
                    <span><Ticket size={16} />{selectedEvent.totalTicketQuantity} Total de ingressos</span>
                    <span><Ticket size={16} />R$ {Number(selectedEvent.ticketValue).toFixed(2)} por ingresso</span>
                  </div>
                </>
              )}
            </div>
            <button type="button" className="organizer-modal-save-btn" onClick={handleCloseModals}>Fechar</button>
          </div>
        </div>
      )}

      {showStatusModal && selectedEvent && (
        <div className="organizer-modal-overlay">
          <div className="organizer-modal-card">
            <div className="organizer-modal-header">
              <h2 className="organizer-modal-title">Alterar Status do Evento</h2>
              <button type="button" className="organizer-modal-close-btn" onClick={handleCloseModals}>✕</button>
            </div>
            <div className="organizer-modal-body-scroll">
              <h3 style={{ textAlign: 'center', marginBottom: '1rem' }}>{selectedEvent.name}</h3>
              <div style={{ display: 'flex', flexDirection: 'column', gap: '0.75rem' }}>
                {[
                  { label: 'Andamento', value: 1 },
                  { label: 'Cancelado', value: 2 },
                  { label: 'Encerrado', value: 3 },
                ].map((option) => (
                  <label key={option.value} style={{
                    display: 'flex', alignItems: 'center', gap: '0.75rem',
                    padding: '0.875rem 1rem',
                    background: selectedStatus === option.value ? '#f0f0f0' : '#f9fafb',
                    border: `1.5px solid ${selectedStatus === option.value ? '#0d0d0d' : '#e5e7eb'}`,
                    borderRadius: '8px', cursor: 'pointer'
                  }}>
                    <input type="radio" name="status" value={option.value}
                      checked={selectedStatus === option.value}
                      onChange={() => setSelectedStatus(option.value)} />
                    <span>{option.label}</span>
                  </label>
                ))}
              </div>
              {statusFeedback.message && (
                <p style={{ marginTop: '1rem', textAlign: 'center',
                  color: statusFeedback.type === 'success' ? '#16a34a' : '#dc2626' }}>
                  {statusFeedback.message}
                </p>
              )}
            </div>
            <button type="button" className="organizer-modal-save-btn"
              onClick={handleStatusSubmit} disabled={selectedStatus === null || isSavingStatus}>
              {isSavingStatus ? 'Salvando...' : 'Salvar Status'}
            </button>
          </div>
        </div>
      )}
    </div>
  );
}

export default OrganizerEvents;