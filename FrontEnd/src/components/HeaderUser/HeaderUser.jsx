import { useState, useRef, useEffect } from "react";
import { ChevronDown, LogOut, UserCircle2 } from "lucide-react";
import "./HeaderUser.css";

export default function HeaderUser() {
  const [open, setOpen] = useState(false);
  const dropdownRef = useRef(null);

  // Fecha o dropdown ao clicar fora
  useEffect(() => {
    function handleClickOutside(event) {
      if (
        dropdownRef.current &&
        !dropdownRef.current.contains(event.target)
      ) {
        setOpen(false);
      }
    }

    document.addEventListener("mousedown", handleClickOutside);

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  const user = {
    firstName: "Noah",
    profileImage:
      "https://i.pravatar.cc/100?img=12",
  };

  return (
    <header className="w-full border-b bg-white px-8 py-4">
      <div className="mx-auto flex max-w-7xl items-center justify-between">
        
        {/* Logo */}
        <div className="flex items-center gap-3">
          <div className="flex h-10 w-10 items-center justify-center rounded-xl bg-black text-white font-bold">
            IJ
          </div>

          <h1 className="text-xl font-semibold text-zinc-900">
            IngressoJa
          </h1>
        </div>

        {/* Perfil */}
        <div className="relative" ref={dropdownRef}>
          <button
            onClick={() => setOpen(!open)}
            className="flex items-center gap-3 rounded-full border border-zinc-200 bg-white px-3 py-2 transition hover:bg-zinc-100"
          >
            <img
              src={user.profileImage}
              alt="Perfil"
              className="h-9 w-9 rounded-full object-cover"
            />

            <span className="font-medium text-zinc-800">
              {user.firstName}
            </span>

            <ChevronDown
              size={18}
              className={`transition ${
                open ? "rotate-180" : ""
              }`}
            />
          </button>

          {/* Dropdown */}
          {open && (
            <div className="absolute right-0 mt-3 w-56 overflow-hidden rounded-2xl border border-zinc-200 bg-white shadow-lg">
              
              <button className="flex w-full items-center gap-3 px-4 py-3 text-sm text-zinc-700 transition hover:bg-zinc-100">
                <UserCircle2 size={18} />
                Atualizar dados
              </button>

              <button className="flex w-full items-center gap-3 px-4 py-3 text-sm text-red-500 transition hover:bg-red-50">
                <LogOut size={18} />
                Sair
              </button>
            </div>
          )}
        </div>
      </div>
    </header>
  );
}