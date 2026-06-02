import { useEffect, useState } from "react";
import HeaderUser from "../../components/HeaderUser/HeaderUser";
import { getStoredUserId } from "../../utils/auth";
import { getSalesByUser } from "../../api/sales";
import { getEventById, getStateCode } from "../../api/events";
import "./PurchasesPage.css";

export default function PurchasesPage() {
  const [tickets, setTickets] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const mockTicket = {
    id: 'mock-ticket-semanca-01',
    code: 'TICKET-SEMANCA-01',
    eventId: 'permanent-semanca-1',
    eventName: 'Show da SeManca e SeMata',
    eventDescription: 'Uma noite inesquecível onde a SeManca toca trompete com os pés e a SeMata ensina passos de dança proibidos até pela física.',
    eventCity: 'Cidade Imaginária',
    eventState: 'ZZ',
    eventStreet: 'Praça do Caos Glorioso',
    eventNeighborhood: 'Vila do Riso',
    eventNumber: '1',
    eventDate: '31/12/2026',
    eventHour: '23:59',
    bannerImage: 'https://i.pinimg.com/736x/c5/53/79/c55379996a160a72d08150c3b05db17d.jpg',
    quantity: 2,
    unitPrice: 99.9,
    totalPrice: 199.8,
    purchasedAt: new Date().toISOString(),
    status: 'Pago'
  };

  useEffect(() => {
    async function fetchPurchases() {
      try {
        const userId = getStoredUserId();
        const sales = await getSalesByUser(userId);

        if (!sales || sales.length === 0) {
          setTickets([mockTicket]);
          return;
        }

        const fetchedTickets = await Promise.all(
          sales.map(async (sale) => {
            const event = await getEventById(sale.eventId);
            return {
              id: sale.id,
              code: `TICKET-${sale.id}`,
              eventId: sale.eventId,
              eventName: event?.name ?? "Evento desconhecido",
              eventDescription: event?.description ?? "",
              eventCity: event?.city ?? "",
              eventState: getStateCode(event?.state),
              eventStreet: event?.street ?? "",
              eventNeighborhood: event?.neighborhood ?? "",
              eventNumber: event?.number ?? "",
              eventDate: event?.date ?? "",
              eventHour: event?.hour ?? "",
              bannerImage: event?.bannerImage ?? "",
              quantity: sale.selectedTicketsUser,
              unitPrice: event?.ticketValue ?? 0,
              totalPrice: sale.totalPrice,
              purchasedAt: sale.createdAt,
              status: sale.saleStatus,
            };
          })
        );

        const hasMock = fetchedTickets.some((t) => String(t.code) === mockTicket.code);
        setTickets(hasMock ? fetchedTickets : [mockTicket, ...fetchedTickets]);
      } catch (err) {
        setError("Não foi possível carregar suas compras. Tente novamente.");
        console.error(err);
      } finally {
        setLoading(false);
      }
    }

    fetchPurchases();
  }, []);

  return (
    <div className="purchases-page">
      <HeaderUser />

      <main className="purchases-main">
        <section className="purchases-card">
          <h1 className="purchases-title">Minhas Compras</h1>

          {loading && <p className="purchases-empty">Carregando compras...</p>}

          {error && <p className="purchases-empty">{error}</p>}

          {!loading && !error && tickets.length === 0 && (
            <p className="purchases-empty">Você não possui ingressos comprados.</p>
          )}

          {!loading && !error && tickets.length > 0 && (
            <ul className="purchases-list">
              {tickets.map((t) => (
                <li key={t.id} className="purchase-item">
                  <div className="purchase-left">
                    <img
                      src={t.bannerImage || "https://via.placeholder.com/120x80"}
                      alt={t.eventName}
                    />
                  </div>
                  <div className="purchase-body">
                    <strong className="purchase-event-name">{t.eventName}</strong>
                    <div className="purchase-meta">
                      <span>{t.eventDate}{t.eventHour ? ` • ${t.eventHour}` : ""}</span>
                      <span> • Quantidade: {t.quantity}</span>
                      <span> • R$ {Number(t.totalPrice).toFixed(2).replace(".", ",")}</span>
                    </div>
                    <div className="purchase-code">Código: {t.code}</div>
                  </div>
                  <div className="purchase-right">
                    <div className={`purchase-status ${t.status?.toLowerCase() ?? ""}`}>
                      {t.status}
                    </div>
                  </div>
                </li>
              ))}
            </ul>
          )}
        </section>
      </main>
    </div>
  );
}