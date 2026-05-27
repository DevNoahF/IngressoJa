import { useState } from 'react'
import './ChangeEventStatusPage.css'

const STATUS_OPTIONS = [
  { label: 'Andamento', value: 1, className: 'andamento' },
  { label: 'Cancelado', value: 2, className: 'cancelado' },
  { label: 'Encerrado', value: 3, className: 'encerrado' },
]

function ChangeEventStatusPage() {
  const [eventId, setEventId] = useState('')
  const [selectedStatus, setSelectedStatus] = useState(null)
  const [loading, setLoading] = useState(false)
  const [feedback, setFeedback] = useState(null)

  const handleSubmit = async () => {
    if (!eventId || selectedStatus === null) return

    setLoading(true)
    setFeedback(null)

    try {
      const response = await fetch(`http://localhost:5000/events/${eventId}`, {
        method: 'PATCH',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ status: selectedStatus }),
      })

      if (!response.ok) throw new Error('Erro ao atualizar status')

      setFeedback({ type: 'success', message: 'Status atualizado com sucesso!' })
      setEventId('')
      setSelectedStatus(null)
    } catch (err) {
      setFeedback({ type: 'error', message: err.message || 'Erro ao atualizar status' })
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="change-status-container">
      <div className="change-status-card">
        <div className="change-status-icon">🎭</div>
        <h1 className="change-status-title">Alterar Status do Evento</h1>
        <p className="change-status-subtitle">Atualize o estado atual do evento</p>

        <div className="form-group">
          <label className="form-label">ID do Evento</label>
          <input
            className="form-input"
            type="text"
            placeholder="Digite o ID do evento"
            value={eventId}
            onChange={(e) => setEventId(e.target.value)}
          />
        </div>

        <div className="form-group">
          <label className="form-label">Novo Status</label>
          <div className="status-options">
            {STATUS_OPTIONS.map((option) => (
              <label
                key={option.value}
                className={`status-option ${selectedStatus === option.value ? 'selected' : ''}`}
              >
                <input
                  type="radio"
                  name="status"
                  value={option.value}
                  checked={selectedStatus === option.value}
                  onChange={() => setSelectedStatus(option.value)}
                />
                <span className={`status-dot ${option.className}`} />
                <span className="status-label">{option.label}</span>
              </label>
            ))}
          </div>
        </div>

        <button
          className="btn-submit"
          onClick={handleSubmit}
          disabled={!eventId || selectedStatus === null || loading}
        >
          {loading ? 'Salvando...' : 'Salvar Alteração'}
        </button>

        {feedback && (
          <div className={`feedback-message ${feedback.type}`}>
            {feedback.message}
          </div>
        )}
      </div>
    </div>
  )
}

export default ChangeEventStatusPage