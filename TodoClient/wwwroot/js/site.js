// POST
fetch('https://localhost:7293/todo', {
    method: "POST",

    body: JSON.stringify({ name: 'Learn Java' }),

    headers: {
        "Content-type": "application/json; charset=UTF-8"
    }
})
	.then(r => r.json())
    .then(data => console.log('after post: ', data));


// GET
fetch('https://localhost:7293/todo')
	.then(r => r.json())
    .then(data => console.log('after get: ', data));
