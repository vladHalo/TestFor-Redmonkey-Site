async function removeBtn(_id,elem) {
    const url = "/Calculators/RemoveCalcJson";
    const data = {
        id: _id
    }

    const response = await fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const json = await response.json();
    console.log(json.Data);
    var idElem = document.getElementById(elem + _id);
    idElem.innerHTML = "";
    console.log(elem += _id);
}