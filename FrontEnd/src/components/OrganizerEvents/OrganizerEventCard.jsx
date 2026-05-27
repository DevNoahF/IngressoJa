import "./OrganizerEventCard.css";
import { Calendar, Clock, MapPin } from "lucide-react";

function OrganizerEventCard({ event, onEdit, onRevenue, onPhotoClick }) {
  return (
    <article className="organizer-event-shell">
      <div className="organizer-event-card">
        <img 
          src={event.bannerImage} 
          alt={event.name} 
          className="organizer-event-image" 
          onClick={onPhotoClick}
          style={{ cursor: 'pointer' }}
        />
        <div className="organizer-event-meta">
          <h3>{event.name}</h3>

          <div className="organizer-event-details">
            <span>
              <MapPin size={15} />
              {event.location}
            </span>

            <span>
              <Calendar size={15} />
              {event.formattedDate}
            </span>

            <span>
              <Clock size={15} />
              {event.hour}
            </span>
          </div>

          <button type="button" className="organizer-event-edit-button" onClick={onEdit}>
            editar evento
          </button>

          <button type="button" className="organizer-event-revenue-button" onClick={onRevenue}>
            ver receita
          </button>
        </div>
      </div>
    </article>
  );
}

export default OrganizerEventCard;