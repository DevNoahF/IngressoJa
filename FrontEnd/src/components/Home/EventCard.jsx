import "./EventCard.css";
import { MapPin, Calendar, Clock } from "lucide-react";

function EventCard({ event }) {
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

          <span>
            <Clock size={15} />
            {event.hour}
          </span>
        </div>

        <button>Ler Mais</button>
      </div>
    </div>
  );
}

export default EventCard;