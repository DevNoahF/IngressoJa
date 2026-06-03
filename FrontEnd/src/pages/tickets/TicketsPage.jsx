import { useEffect, useState } from "react";
import { Ticket } from "lucide-react";
import HeaderUser from "../../components/HeaderUser/HeaderUser";
import Footer from "../../components/Home/Footer";
import { getTicketsByUserId } from "../../api/tickets";
import "./TicketsPage.css";

function TicketsPage() {
  const [tickets, setTickets] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    let isMounted = true;

    async function loadTickets() {
      const userId = window.localStorage.getItem("userId") ?? "";

      if (!userId) {
        if (isMounted) {
          setTickets([]);
          setError("Nenhum usuário autenticado foi encontrado.");
          setIsLoading(false);
        }
        return;
      }

      try {
        setIsLoading(true);
        setError("");

        const response = await getTicketsByUserId(userId);

        if (!isMounted) return;

        setTickets(Array.isArray(response) ? response : []);
      } catch (requestError) {
        if (isMounted) {
          setTickets([]);
          setError(requestError instanceof Error ? requestError.message : "Não foi possível carregar seus ingressos.");
        }
      } finally {
        if (isMounted) setIsLoading(false);
      }
    }

    loadTickets();

    return () => {
      isMounted = false;
    };
  }, []);

  return (
    <div className="tickets-page">
      <HeaderUser />

      <main className="tickets-page-content">
        <section className="tickets-header">
          <div>
            <h1>Seus ingressos</h1>
            <p>Uma listinha com os ingressos comprados pelo usuário logado.</p>
          </div>
        </section>

        {isLoading ? <p className="tickets-status-message">Carregando ingressos...</p> : null}
        {error ? <p className="tickets-status-message error">{error}</p> : null}

        {!isLoading && !error && tickets.length === 0 ? (
          <p className="tickets-status-message">Você não possui ingressos comprados.</p>
        ) : null}

        {!isLoading && tickets.length > 0 ? (
          <section className="tickets-list">
            {tickets.map((ticket) => (
              <article className="ticket-list-item" key={ticket.code}>
                <div className="ticket-list-photo">
                  <img
                    src={ticket.bannerImage ?? "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f"}
                    alt={ticket.eventName ?? "Foto do evento"}
                  />
                </div>

                <div className="ticket-card-body">
                  <div className="ticket-card-header">
                    <div>
                      <h2>{ticket.eventName ?? "Ingresso confirmado"}</h2>
                      <p className="ticket-description">
                        {ticket.eventDescription ?? ""}
                      </p>
                    </div>

                    <span className="ticket-code-badge">{ticket.code}</span>
                  </div>

                  <div className="ticket-card-info">
                    <span>
                      <Ticket size={16} />
                      Código {ticket.code}
                    </span>
                    <span>
                      <Ticket size={16} />
                      Usuário {ticket.userId}
                    </span>
                  </div>
                </div>
              </article>
            ))}
          </section>
        ) : null}
      </main>

      <Footer />
    </div>
  );
}

export default TicketsPage;