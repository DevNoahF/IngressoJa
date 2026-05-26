import "./OrganizerEventsToolbar.css";
import { Search } from "lucide-react";

const filterOptions = [
  { value: "all", label: "Todos" },
  { value: "upcoming", label: "Próximos" },
  { value: "finished", label: "Encerrados" },
];

function OrganizerEventsToolbar({ searchTerm, onSearchTermChange, activeFilter, onFilterChange, eventCount }) {
  return (
    <section className="organizer-toolbar">
      <div className="organizer-toolbar-copy">
        <span className="organizer-toolbar-kicker">Central do organizer</span>
        <h2>Eventos vinculados a este usuário</h2>
        <p>
          Filtre, consulte e acompanhe a agenda dos eventos já criados sem sair do front-end.
        </p>
      </div>

      <div className="organizer-toolbar-controls">
        <label className="organizer-search">
          <Search size={18} />
          <input
            type="search"
            placeholder="Buscar por nome, cidade ou data"
            value={searchTerm}
            onChange={(event) => onSearchTermChange(event.target.value)}
          />
        </label>

        <div className="organizer-filter-group" role="tablist" aria-label="Filtros de eventos">
          {filterOptions.map((option) => (
            <button
              key={option.value}
              type="button"
              className={`organizer-filter-button ${activeFilter === option.value ? "active" : ""}`}
              onClick={() => onFilterChange(option.value)}
            >
              {option.label}
            </button>
          ))}
        </div>

        <span className="organizer-toolbar-count">{eventCount} evento(s) exibido(s)</span>
      </div>
    </section>
  );
}

export default OrganizerEventsToolbar;