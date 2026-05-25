import "./RegisterForm.css";

import { useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Upload,
  UserPlus,
} from "lucide-react";
import { registerUser } from "../../api/users";

const initialFormData = {
  firstName: "",
  lastName: "",
  cpf: "",
  dateBirth: "",
  email: "",
  password: "",
  confirmPassword: "",
  photoProfile: "",
};

function RegisterForm() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState(initialFormData);
  const [feedback, setFeedback] = useState({ type: "", message: "" });
  const [isSubmitting, setIsSubmitting] = useState(false);

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

    if (formData.password !== formData.confirmPassword) {
      setFeedback({
        type: "error",
        message: "A senha e a confirmação precisam ser iguais.",
      });
      return;
    }

    setIsSubmitting(true);

    try {
      await registerUser({
        firstName: formData.firstName.trim(),
        lastName: formData.lastName.trim(),
        cpf: { value: formData.cpf.replace(/\D/g, "") },
        photoProfile: { value: formData.photoProfile.trim() },
        email: { value: formData.email.trim() },
        password: { value: formData.password },
        dateBirth: formData.dateBirth,
      });

      setFeedback({
        type: "success",
        message: "Cadastro realizado com sucesso.",
      });

      setFormData(initialFormData);
      navigate("/");
    } catch (error) {
      setFeedback({
        type: "error",
        message: error instanceof Error ? error.message : "Não foi possível concluir o cadastro.",
      });
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <div className="register-card">
      <div className="register-header">
        <div className="register-icon">
          <UserPlus size={28} />
        </div>

        <h1>Cadastro de Usuário</h1>

        <p>
          Crie sua conta para comprar ingressos
        </p>
      </div>

      <div className="avatar-upload">
        {formData.photoProfile ? (
          <img src={formData.photoProfile} alt="Preview da foto de perfil" />
        ) : (
          <Upload size={38} />
        )}
      </div>

      <form className="register-form" onSubmit={handleSubmit}>
        <div className="row">
          <div className="input-group">
            <label htmlFor="firstName">Nome</label>
            <input
              id="firstName"
              name="firstName"
              type="text"
              placeholder="João"
              value={formData.firstName}
              onChange={handleChange}
              required
            />
          </div>

          <div className="input-group">
            <label htmlFor="lastName">Sobrenome</label>
            <input
              id="lastName"
              name="lastName"
              type="text"
              placeholder="Silva"
              value={formData.lastName}
              onChange={handleChange}
              required
            />
          </div>
        </div>

        <div className="row">
          <div className="input-group">
            <label htmlFor="cpf">CPF</label>
            <input
              id="cpf"
              name="cpf"
              type="text"
              placeholder="000.000.000-00"
              value={formData.cpf}
              onChange={handleChange}
              required
            />
          </div>

          <div className="input-group">
            <label htmlFor="dateBirth">Data de Nascimento</label>
            <input
              id="dateBirth"
              name="dateBirth"
              type="date"
              value={formData.dateBirth}
              onChange={handleChange}
              required
            />
          </div>
        </div>

        <div className="input-group">
          <label htmlFor="email">Email</label>

          <input
            id="email"
            name="email"
            type="email"
            placeholder="seu@email.com"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </div>

        <div className="input-group">
          <label htmlFor="photoProfile">URL da foto de perfil</label>
          <input
            id="photoProfile"
            name="photoProfile"
            type="url"
            placeholder="https://..."
            value={formData.photoProfile}
            onChange={handleChange}
            required
          />
          <small className="field-hint">Cole o link direto da imagem para manter o cadastro simples por enquanto.</small>
        </div>

        <div className="row">
          <div className="input-group">
            <label htmlFor="password">Senha</label>
            <input
              id="password"
              name="password"
              type="password"
              placeholder="••••••••"
              value={formData.password}
              onChange={handleChange}
              required
            />
          </div>

          <div className="input-group">
            <label htmlFor="confirmPassword">Confirmar Senha</label>
            <input
              id="confirmPassword"
              name="confirmPassword"
              type="password"
              placeholder="••••••••"
              value={formData.confirmPassword}
              onChange={handleChange}
              required
            />
          </div>
        </div>

        {feedback.message ? (
          <p className={`form-feedback ${feedback.type}`}>{feedback.message}</p>
        ) : null}

        <button className="register-btn" type="submit" disabled={isSubmitting}>
          {isSubmitting ? "Cadastrando..." : "Cadastrar"}
        </button>

        <p className="login-text">
          Já tem uma conta? <span>Faça login</span>
        </p>
      </form>
    </div>
  );
}

export default RegisterForm;