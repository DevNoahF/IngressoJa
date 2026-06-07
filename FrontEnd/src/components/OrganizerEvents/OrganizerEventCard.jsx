import "./OrganizerEventCard.css";
import { Calendar, Clock, MapPin, Ticket, Tag } from "lucide-react";

function OrganizerEventCard({ event, onEdit, onRevenue, onPhotoClick, onDelete, onStatus }) {
  return (
    <article className="organizer-event-shell">
      <div className="organizer-event-card">
        {onDelete ? (
          <button
            type="button"
            className="organizer-event-delete"
            aria-label={`Excluir ${event.name}`}
            title="Excluir evento"
            onClick={onDelete}
          >
            ×
          </button>
        ) : null}

        <img
          src={event.bannerImage}
          alt={event.name}
          className="organizer-event-image"
          onClick={onPhotoClick}
          style={{ cursor: "pointer" }}
        />

        <div className="organizer-event-meta">
          <h3>{event.name}</h3>

          <div className="organizer-event-details">
            <span><MapPin size={15} />{event.location}</span>
            <span><Calendar size={15} />{event.formattedDate}</span>

            {event.ticketValue !== undefined && event.ticketValue !== null ? (
              <span><Ticket size={15} />R$ {Number(event.ticketValue).toFixed(2)}</span>
            ) : null}

            {event.totalTicketQuantity !== undefined && event.totalTicketQuantity !== null ? (
              <span><Ticket size={15} />{event.totalTicketQuantity} ingressos</span>
            ) : null}
          </div>

          <button type="button" className="organizer-event-edit-button" onClick={onEdit}>
            Editar Evento
          </button>

          <button type="button" className="organizer-event-revenue-button" onClick={onRevenue}>
            Ver Receita
          </button>

         {/* {onStatus ? (
            <button type="button" className="organizer-event-status-button" onClick={onStatus}>
              <Tag size={14} /> Alterar Status
            </button>
          ) : null}
           */}
        </div>
      </div>
    </article>
  );
}

export default OrganizerEventCard;