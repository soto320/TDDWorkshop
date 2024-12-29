import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '10s', target: 5 }, // Ramp-up to 5 users in 10 seconds
        { duration: '30s', target: 5 }, // Stay at 5 users for 30 seconds
        { duration: '10s', target: 0 }, // Ramp-down to 0 users in 10 seconds
    ],
};

export default function () {
    const url = 'http://localhost:59887/api/Account/Transfer';
    const payload = JSON.stringify({
        SenderId: 1,
        RecipientId: 2,
        Amount: 1500,
        Description: "sample string 4",
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const res = http.post(url, payload, params);

    // Assertions to validate the response
    check(res, {
        'is status 200': (r) => r.status === 200,
        'response contains success message': (r) => JSON.parse(r.body).Message === 'Transferencia exitosa',
    });

    // Pause for 1 second between requests
    sleep(1);
}