

function formSubmit() {
    const uri = 'api/TodoItems';
    const email = document.getElementById("exampleInputEmail1").value
    const password = document.getElementById("exampleInputPassword1").innerHTML;

    const data = {
        Email: email,
        Password: password
    }

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}
};