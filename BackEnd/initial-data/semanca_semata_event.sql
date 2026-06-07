-- -- Template para inserção de Eventos
 INSERT INTO Events (
     Id, 
     Name, 
     Description, 
     Street, 
     Neighborhood, 
     City, 
     Number, 
     State, 
    Date, 
     Hour, 
     TicketValue, 
     TotalTicketQuantity, 
     Status, 
     BannerImage, 
     UserId, 
     CreatedAt
 ) VALUES (
     UUID(), -- Ou use um GUID fixo: '550e8400-e29b-41d4-a716-446655440000'
     'Show da Semanca e Semata', 
     'Semanca & Semata apresentam um show sertanejo sombrio e irreverente, misturando sofrência, mistério e humor. Uma experiência única com músicas marcantes, visual impactante e muita atitude no palco.', 
     'Av. João Ramalho', 
     'Parque Sao Jorge', 
     'São Paulo', 
     1306, 
     1, -- Representando o índice do StatesEnum
     '2026-12-31', 
     '20:00:00', 
     150.00, 
     1200, 
     0, -- Representando EventStatusEnum.Andamento
     'https://i.pinimg.com/736x/d9/7a/bc/d97abc082d68c33519d3188e32fc32a9.jpg', 
     '00000000-0000-0000-0000-000000000000', -- ID do Usuário (Organizer)
    UTC_TIMESTAMP()
 );