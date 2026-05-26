import "./OrganizerStats.css";

function OrganizerStats({ stats }) {
  return (
    <section className="organizer-stats" aria-label="Resumo dos eventos do organizer">
      <article className="organizer-stat-card">
        <span className="organizer-stat-label">Eventos vinculados</span>
        <strong className="organizer-stat-value">{stats.totalEvents}</strong>
        <p>Eventos cadastrados com o seu usuário.</p>
      </article>

      <article className="organizer-stat-card">
        <span className="organizer-stat-label">Próximos</span>
        <strong className="organizer-stat-value">{stats.upcomingEvents}</strong>
        <p>Eventos ainda abertos na agenda.</p>
      </article>

      <article className="organizer-stat-card">
        <span className="organizer-stat-label">Potencial bruto</span>
        <strong className="organizer-stat-value">R$ {stats.potentialRevenue}</strong>
        <p>Estimativa com base no valor e na capacidade informados.</p>
      </article>
    </section>
  );
}

export default OrganizerStats;