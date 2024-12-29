import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  vus: 5, // Usuarios virtuales concurrentes
  duration: '30s', // Duración de la prueba
};

export default function () {
  // URL de la API
  const url = 'http://localhost:59887/api/Account/Transfer';

  // Cuerpo de la solicitud
  const payload = JSON.stringify({
    SenderId: 1,
    RecipientId: 2,
    Amount: 1500,
    Description: "sample string 4",
  });

  // Encabezados de la solicitud
  const params = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  // Enviar la solicitud POST
  const response = http.post(url, payload, params);

  // Validar la respuesta
  check(response, {
    'Estado es 200': (r) => r.status === 200,
    'Transferencia exitosa': (r) =>
      JSON.parse(r.body).success === true &&
      JSON.parse(r.body).message === 'Transferencia exitosa.',
    'Saldo del remitente correcto': (r) =>
      JSON.parse(r.body).senderBalance === 900.0,
    'Saldo del destinatario correcto': (r) =>
      JSON.parse(r.body).recipientBalance === 600.0,
  });

  // Pausa de 1 segundo entre solicitudes
  sleep(1);
}