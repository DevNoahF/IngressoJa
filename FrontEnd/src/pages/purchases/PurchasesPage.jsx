import { useEffect, useState } from "react";
import HeaderUser from "../../components/HeaderUser/HeaderUser";
import { getStoredUserId } from "../../utils/auth";
import { getSalesByUser } from "../../api/sales";
import { getEventById, getStateCode } from "../../api/events";
import "./PurchasesPage.css";

const STATUS_MAP = {
  Pending: "Pendente",
  Approved: "Pago",
  Cancelled: "Cancelado",
};

export default function PurchasesPage() {
  const [tickets, setTickets] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function fetchPurchases() {
      try {
        const userId = getStoredUserId();
        const sales = await getSalesByUser(userId);

        if (!sales || sales.length === 0) {
          setTickets([]);
          return;
        }

        const fetchedTickets = await Promise.all(
          sales.map(async (sale) => {
            const event = await getEventById(sale.eventId);
            const statusLabel = STATUS_MAP[sale.saleStatus] ?? sale.saleStatus;

            return {
              id: sale.id,
              code: sale.ticketId ? `TICKET-${sale.ticketId}` : `TICKET-${sale.id}`,
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
              status: statusLabel,
            };
          })
        );

        setTickets(fetchedTickets);
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

          {loading ? <p className="purchases-empty">Carregando compras...</p> : null}
          {error ? <p className="purchases-empty">{error}</p> : null}

          {!loading && !error && tickets.length === 0 ? (
            <p className="purchases-empty">Você não possui ingressos comprados.</p>
          ) : null}

          {!loading && !error && tickets.length > 0 ? (
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
          ) : null}
        </section>
      </main>
    </div>
  );
}