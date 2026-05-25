import "./CreateEventForm.css";

import { useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { CalendarDays, Upload } from "lucide-react";
import { createEvent, statesOptions } from "../../api/events";

const initialFormData = {
  bannerImage: "",
  name: "",
  description: "",
  street: "",
  number: "",
  neighborhood: "",
  city: "",
  state: "",
  date: "",
  hour: "",
  ticketValue: "",
  totalTicketQuantity: "",
};

function CreateEventForm() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState(initialFormData);
  const [feedback, setFeedback] = useState({ type: "", message: "" });
  const [isSubmitting, setIsSubmitting] = useState(false);

  const selectedStateLabel = useMemo(() => {
    const option = statesOptions.find((item) => item.value === Number(formData.state));
    return option ? `${option.code} - ${option.name}` : "Selecione o estado";
  }, [formData.state]);

  function handleChange(event) {
    const { name, value } = event.target;
    setFormData((currentData) => ({
      ...currentData,
      [name]: value,
    }));
  }

  async function handleSubmit(event) {
    event.preventDefault();
    setFeedback({ type: "", message: "" });

    const organizerId = localStorage.getItem("userId")?.trim() ?? "";

    if (!organizerId) {
      setFeedback({
        type: "error",
        message: "Faça login para criar o evento. O ID do organizador será preenchido automaticamente.",
      });
      return;
    }

    setIsSubmitting(true);

    try {
      await createEvent({
        name: formData.name.trim(),
        description: formData.description.trim(),
        street: formData.street.trim(),
        neighborhood: formData.neighborhood.trim(),
        city: formData.city.trim(),
        number: Number(formData.number),
        state: Number(formData.state),
        date: formData.date,
        hour: formData.hour,
        ticketValue: Number(formData.ticketValue),
        totalTicketQuantity: Number(formData.totalTicketQuantity),
        bannerImage: formData.bannerImage.trim(),
        userId: organizerId,
      });

      setFeedback({
        type: "success",
        message: "Evento criado com sucesso.",
      });

      setFormData(initialFormData);
      navigate("/");
    } catch (error) {
      setFeedback({
        type: "error",
        message: error instanceof Error ? error.message : "Não foi possível criar o evento.",
      });
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <div className="event-form-card">
      <div className="event-form-header">
        <div className="event-icon">
          <CalendarDays size={26} />
        </div>

        <h1>Criar Novo Evento</h1>
        <p>Preencha os dados do seu evento</p>
      </div>

      <form className="event-form" onSubmit={handleSubmit}>
        <div className="input-group">
          <label htmlFor="bannerImage">Banner do Evento</label>
          <div className="banner-preview-box">
            {formData.bannerImage ? (
              <img src={formData.bannerImage} alt="Preview do banner do evento" />
            ) : (
              <div className="banner-preview-placeholder">
                <Upload size={34} />
                <span>Adicione a URL do banner para ver o preview aqui</span>
              </div>
            )}
          </div>
          <input
            id="bannerImage"
            name="bannerImage"
            type="url"
            placeholder="https://..."
            value={formData.bannerImage}
            onChange={handleChange}
            required
          />
          <small className="field-hint">Use uma URL pública da imagem até o upload ficar integrado ao backend.</small>
        </div>

        <div className="input-group">
          <label htmlFor="name">Nome do Evento</label>
          <input
            id="name"
            name="name"
            type="text"
            placeholder="Festival de Rock 2026"
            value={formData.name}
            onChange={handleChange}
            required
          />
        </div>

        <div className="input-group">
          <label htmlFor="description">Descrição</label>
          <textarea
            id="description"
            name="description"
            placeholder="Descreva os detalhes do evento..."
            value={formData.description}
            onChange={handleChange}
            required
          />
        </div>

        <div className="row">
          <div className="input-group flex-2">
            <label htmlFor="street">Rua/Avenida</label>
            <input
              id="street"
              name="street"
              type="text"
              placeholder="Av. Paulista"
              value={formData.street}
              onChange={handleChange}
              required
            />
          </div>

          <div className="input-group flex-1">
            <label htmlFor="number">Número</label>
            <input
              id="number"
              name="number"
              type="number"
              min="0"
              placeholder="1000"
              value={formData.number}
              onChange={handleChange}
              required
            />
          </div>
        </div>

        <div className="row">
          <div className="input-group">
            <label htmlFor="neighborhood">Bairro</label>
            <input
              id="neighborhood"
              name="neighborhood"
              type="text"
              placeholder="Bela Vista"
              value={formData.neighborhood}
              onChange={handleChange}
              required
            />
          </div>

          <div className="input-group">
            <label htmlFor="city">Cidade</label>
            <input
              id="city"
              name="city"
              type="text"
              placeholder="São Paulo"
              value={formData.city}
              onChange={handleChange}
              required
            />
          </div>
        </div>

        <div className="input-group">
          <label htmlFor="state">Estado</label>
          <select
            id="state"
            name="state"
            value={formData.state}
            onChange={handleChange}
            required
          >
            <option value="">Selecione o estado</option>
            {statesOptions.map((state) => (
              <option key={state.value} value={state.value}>
                {state.code} - {state.name}
              </option>
            ))}
          </select>
          <small className="field-hint">Selecionado: {selectedStateLabel}</small>
        </div>

        <div className="row">
          <div className="input-group">
            <label htmlFor="date">Data</label>
            <input
              id="date"
              name="date"
              type="date"
              value={formData.date}
              onChange={handleChange}
              required
            />
          </div>

          <div className="input-group">
            <label htmlFor="hour">Horário</label>
            <input
              id="hour"
              name="hour"
              type="time"
              value={formData.hour}
              onChange={handleChange}
              required
            />
          </div>
        </div>

        <div className="row">
          <div className="input-group">
            <label htmlFor="ticketValue">Valor do ingresso</label>
            <input
              id="ticketValue"
              name="ticketValue"
              type="number"
              min="0"
              step="0.01"
              placeholder="0,00"
              value={formData.ticketValue}
              onChange={handleChange}
              required
            />
          </div>

          <div className="input-group">
            <label htmlFor="totalTicketQuantity">Quantidade total</label>
            <input
              id="totalTicketQuantity"
              name="totalTicketQuantity"
              type="number"
              min="1"
              placeholder="100"
              value={formData.totalTicketQuantity}
              onChange={handleChange}
              required
            />
          </div>
        </div>

        {feedback.message ? (
          <p className={`form-feedback ${feedback.type}`}>{feedback.message}</p>
        ) : null}

        <button className="submit-btn" type="submit" disabled={isSubmitting}>
          {isSubmitting ? "Criando..." : "Criar Evento"}
        </button>
      </form>
    </div>
  );
}

export default CreateEventForm;