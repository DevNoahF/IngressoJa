# IngressoJa 🎟️

Plataforma full-stack para venda de ingressos de eventos. Usuários podem comprar ingressos via PIX (simulado) e organizadores podem criar e gerenciar eventos.

## Stack

**Backend:** ASP.NET Core (C#), EF Core + MySQL, JWT, BCrypt, Swagger  
**Frontend:** React 19 + Vite, React Router, Lucide Icons  
**Infra:** Docker Compose

## Funcionalidades

- **Usuários:** cadastro/login, comprar ingressos, histórico de compras, visualizar ingressos
- **Organizadores:** CRUD de eventos, resumo de vendas por evento
- **Autenticação:** JWT com dois perfis (User/Organizer), senhas com BCrypt
- **Pagamento:** Fluxo PIX simulado (QR Code + confirmação)
- **API:** Arquitetura DDD com value objects e domain events

## Como rodar

```bash
docker compose up -d
```

## Estrutura

```
BackEnd/     → API .NET (DDD em bounded contexts: Events, Sales)
FrontEnd/    → SPA React (Vite)
```

## Endpoints principais

| Método | Rota | Descrição |
|---|---|---|
| POST | `/login` | Autenticação |
| POST | `/register` | Cadastro (user/organizer) |
| GET/POST | `/events` | Listar/criar eventos |
| POST | `/sales` | Criar venda |
| PATCH | `/sales/{id}/status` | Aprovar venda |
| GET | `/tickets/user/{userId}` | Ingressos do usuário |
| GET | `/sales/event/{eventId}/summary` | Resumo de vendas do evento |
