import "./Register.css";

import { useLocation } from "react-router-dom";
import RegisterForm from "../../components/RegisterForm/RegisterForm";
import Footer from "../../components/Home/Footer";

function Register() {
  const location = useLocation();
  const path = location.pathname.toLowerCase();
  const isOrganizerRegistration = path.includes("organizer") || path.includes("organizador");
  const roleName = path.includes("admin") ? "Admin" : isOrganizerRegistration ? "Organizador" : "Usuário";

  return (
    <>
      <main className="register-page">
        <RegisterForm isOrganizerRegistration={isOrganizerRegistration} roleName={roleName} />
      </main>
      <Footer />
    </>
  );
}

export default Register;