import "./EventCard.css";
import { MapPin, Calendar, Clock, Ticket } from "lucide-react";

function EventCard({ event, onReadMore }) {
  return (
    <div className="event-card">
      <img src={event.image} alt={event.title} />

      <div className="event-content">
        <h3>{event.title}</h3>

        <div className="event-info">
          <span>
            <MapPin size={15} />
            {event.city}
          </span>

          <span>
            <Calendar size={15} />
            {event.date}
          </span>

          {event.hour ? (
            <span>
              <Clock size={15} />
              {event.hour}
            </span>
          ) : null}

          {event.ticketValue !== undefined && event.ticketValue !== null ? (
            <span>
              <Ticket size={15} />
              R$ {Number(event.ticketValue).toFixed(2)}
            </span>
          ) : null}
        </div>

        <button type="button" onClick={() => onReadMore(event)}>
          Ler Mais
        </button>
      </div>
    </div>
  );
}

export default EventCard;