import "./updateUser.css";
import { useState, useEffect } from "react";
import { UserRound, Loader } from "lucide-react";
import HeaderUser from "../../components/HeaderUser/HeaderUser";
import HeaderOrganizer from "../../components/headerOrganizer/HeaderOrganizer";
import { getStoredRole, getStoredUserId } from "../../utils/auth";
import { getUser, updateUser as updateUserApi } from "../../api/users";

export default function UpdateProfile() {
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    newPassword: "",
    confirmPassword: "",
    photoProfile: "",
  });

  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [feedback, setFeedback] = useState({ type: "", message: "" });
  const role = getStoredRole();

  useEffect(() => {
    const userId = getStoredUserId();
    if (!userId) {
      setFeedback({ type: "error", message: "Usuário não autenticado." });
      setLoading(false);
      return;
    }

    getUser(userId)
      .then((data) => {
        if (data) {
          setFormData({
            firstName: data.firstName || "",
            lastName: data.lastName || "",
            email: data.email?.value || data.email || "",
            photoProfile: data.photoProfile?.value || data.photoProfile || "",
            newPassword: "",
            confirmPassword: "",
          });
        }
      })
      .catch((err) => {
        console.error("Erro ao carregar dados do usuário:", err);
        setFeedback({ type: "error", message: "Erro ao buscar dados no servidor." });
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setFeedback({ type: "", message: "" });

    if (formData.newPassword && formData.newPassword !== formData.confirmPassword) {
      setFeedback({ type: "error", message: "A nova senha e a confirmação precisam ser iguais." });
      return;
    }

    const userId = getStoredUserId();
    if (!userId) {
      setFeedback({ type: "error", message: "Usuário não autenticado." });
      return;
    }

    setSaving(true);

    const payload = {
      firstName: formData.firstName.trim(),
      lastName: formData.lastName.trim(),
      email: { value: formData.email.trim() },
      photoProfile: { value: formData.photoProfile.trim() },
    };

    if (formData.newPassword) {
      payload.password = { value: formData.newPassword };
    }

    try {
      await updateUserApi(userId, payload);
      setFeedback({ type: "success", message: "Dados atualizados com sucesso!" });
      setFormData((prev) => ({
        ...prev,
        newPassword: "",
        confirmPassword: "",
      }));
    } catch (err) {
      console.error("Erro ao atualizar dados:", err);
      setFeedback({
        type: "error",
        message: err instanceof Error ? err.message : "Não foi possível atualizar os dados.",
      });
    } finally {
      setSaving(false);
    }
  };

  return (
    <>
      {role === "Organizer" ? <HeaderOrganizer /> : <HeaderUser />}
      <div className="update-container">
        <div className="update-card">
          <div className="icon-top">
            <UserRound size={28} />
          </div>

          <h1>Atualizar Dados</h1>
          <p>Mantenha suas informações sempre atualizadas</p>

          {loading ? (
            <div style={{ display: "flex", justifyContent: "center", padding: "40px 0" }}>
              <Loader size={36} className="animate-spin" style={{ color: "#020221" }} />
            </div>
          ) : (
            <>
              <div className="avatar-upload">
                {formData.photoProfile ? (
                  <img src={formData.photoProfile} alt="Preview da foto de perfil" />
                ) : (
                  <UserRound size={38} />
                )}
              </div>

              <form className="update-form" onSubmit={handleSubmit}>
                <div className="input-group">
                  <label>Nome</label>
                  <input
                    type="text"
                    name="firstName"
                    placeholder="João"
                    value={formData.firstName}
                    onChange={handleInputChange}
                    required
                  />
                </div>

                <div className="input-group">
                  <label>Sobrenome</label>
                  <input
                    type="text"
                    name="lastName"
                    placeholder="Silva"
                    value={formData.lastName}
                    onChange={handleInputChange}
                    required
                  />
                </div>

                <div className="input-group full-width">
                  <label>Email</label>
                  <input
                    type="email"
                    name="email"
                    placeholder="seu@email.com"
                    value={formData.email}
                    onChange={handleInputChange}
                    required
                  />
                </div>

                <div className="input-group full-width">
                  <label>URL da foto de perfil</label>
                  <input
                    type="url"
                    name="photoProfile"
                    placeholder="https://..."
                    value={formData.photoProfile}
                    onChange={handleInputChange}
                  />
                  <small className="field-hint">Cole o link direto da imagem para manter o cadastro simples por enquanto.</small>
                </div>

                <div className="input-group">
                  <label>Nova Senha</label>
                  <input
                    type="password"
                    name="newPassword"
                    placeholder="Deixe em branco para manter a senha atual"
                    value={formData.newPassword}
                    onChange={handleInputChange}
                  />
                </div>

                <div className="input-group">
                  <label>Confirmar Nova Senha</label>
                  <input
                    type="password"
                    name="confirmPassword"
                    placeholder="Deixe em branco para manter a senha atual"
                    value={formData.confirmPassword}
                    onChange={handleInputChange}
                  />
                </div>

                {feedback.message && (
                  <div className={`form-feedback ${feedback.type}`}>{feedback.message}</div>
                )}

                <button type="submit" className="save-btn" disabled={saving}>
                  {saving ? "Salvando..." : "Salvar Alterações"}
                </button>
              </form>
            </>
          )}
        </div>
      </div>
    </>
  );
}