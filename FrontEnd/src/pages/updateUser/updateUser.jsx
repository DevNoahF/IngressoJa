import "./updateUser.css";
import { useState } from "react";
import { UserRound, Upload } from "lucide-react";

export default function UpdateProfile() {
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    newPassword: "",
    confirmPassword: "",
    photoProfile: "",
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handlePhotoUpload = (e) => {
    const file = e.target.files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (event) => {
        // Converter imagem para string base64
        const imageString = event.target?.result;
        setFormData((prev) => ({
          ...prev,
          photoProfile: imageString,
        }));
      };
      reader.readAsDataURL(file);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("Dados do formulário:", formData);
    // Aqui você pode enviar os dados para o backend
  };
  return (
    <div className="update-container">
      <div className="update-card">
        <div className="icon-top">
          <UserRound size={28} />
        </div>

        <h1>Atualizar Dados</h1>
        <p>Mantenha suas informações sempre atualizadas</p>

        <div className="photo-upload">
          <label htmlFor="photo-input" style={{ cursor: "pointer", display: "flex", alignItems: "center", justifyContent: "center", gap: "8px" }}>
            <Upload size={36} />
            {formData.photoProfile && <span>Foto enviada</span>}
          </label>
          <input
            id="photo-input"
            type="file"
            accept="image/*"
            onChange={handlePhotoUpload}
            style={{ display: "none" }}
          />
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
            />
          </div>

          <div className="input-group">
            <label>Nova Senha</label>
            <input 
              type="password" 
              name="newPassword"
              placeholder="********" 
              value={formData.newPassword}
              onChange={handleInputChange}
            />
          </div>

          <div className="input-group">
            <label>Confirmar Nova Senha</label>
            <input 
              type="password" 
              name="confirmPassword"
              placeholder="********" 
              value={formData.confirmPassword}
              onChange={handleInputChange}
            />
          </div>

          <button type="submit" className="save-btn">
            Salvar Alterações
          </button>
        </form>
      </div>
    </div>
  );
}