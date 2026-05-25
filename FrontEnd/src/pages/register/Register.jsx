import "./Register.css";

import Header from "../../components/Home/Header";
import RegisterForm from "../../components/RegisterForm/RegisterForm";
import Footer from "../../components/Home/Footer";

function Register() {
  return (
    <>
      <Header />

      <main className="register-page">
        <RegisterForm />
      </main>

      <Footer />
    </>
  );
}

export default Register;