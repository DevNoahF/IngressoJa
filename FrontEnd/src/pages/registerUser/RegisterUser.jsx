import "./Register.css";

import { useLocation } from "react-router-dom";
import RegisterForm from "../../components/RegisterForm/RegisterForm";
import Footer from "../../components/Home/Footer";

function RegisterUser() {
  const location = useLocation();
  const path = location.pathname.toLowerCase();
  const isOrganizerRegistration = path.includes("organizer");
  const roleName = "Usuário";

  return (
    <>
      <main className="register-page">
        <RegisterForm isOrganizerRegistration={false} roleName={roleName} />
      </main>
      <Footer />
    </>
  );
}

export default RegisterUser;