import http from 'k6/http';
import { check, sleep } from 'k6';

// Configuración de la prueba
export const options = {
  vus: 5, // Número de usuarios concurrentes
  duration: '30s', // Duración de la prueba
};

export default function () {
  // URL de la API
  const url = 'http://localhost:59887/api/transaction/transfer';

  // Casos de prueba
  const testCases = [
    {
      description: 'Transferencia exitosa',
      payload: JSON.stringify({
        SenderId: 1,
        RecipientId: 2,
        Amount: 150,
        Description: 'Prueba exitosa',
      }),
      expectedStatus: 200,
      expectedMessage: 'Transferencia exitosa.',
    },
    {
      description: 'Saldo insuficiente',
      payload: JSON.stringify({
        SenderId: 1,
        RecipientId: 2,
        Amount: 2000,
        Description: 'Saldo insuficiente',
      }),
      expectedStatus: 400,
      expectedMessage: 'Saldo insuficiente.',
    },
    {
      description: 'Error interno (solicitud inválida)',
      payload: JSON.stringify({
        SenderId: null,
        RecipientId: null,
        Amount: 0,
        Description: '',
      }),
      expectedStatus: 500,
      expectedMessage: 'Ocurrió un error interno al procesar la transferencia.',
    },
  ];

  // Iterar a través de los casos de prueba
  testCases.forEach((testCase) => {
    const params = {
      headers: {
        'Content-Type': 'application/json',
      },
    };

    // Enviar la solicitud
    const response = http.post(url, testCase.payload, params);

    // Verificar la respuesta
    check(response, {
      [`${testCase.description} - Estado correcto`]: (r) =>
        r.status === testCase.expectedStatus,
      [`${testCase.description} - Mensaje correcto`]: (r) =>
        JSON.parse(r.body).Message === testCase.expectedMessage,
    });

    // Pausa entre solicitudes
    sleep(1);
  });
}
