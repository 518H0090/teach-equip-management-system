const url_api = 'https://localhost:7112/api'


const fetchTool = async () => {
    fetch(`${url_api}/toolmanage/all-tools`)
    .then(response => {
        if (!response.ok) {
        throw new Error('Network response was not ok ' + response.statusText);
        }
        return response.json();
    })
    .then(data => {
        if (data.statusCode == 200) {
            let ui = document.querySelector('.x_content tbody');
           console.log(ui)
           console.log(data.data)

           ui.innerHTML = data.data.map(x => {
            return `<tr class="pointer">
                                                <td>${x.id} </td>
                                                <td>${x.toolName} </td>
                                                <td>${x.description}</td>
                                                <td>
                                                    <button class="btn btn-success" type="button">View</button>
                                                    <button class="btn btn-warning" type="button">Update</button>
                                                    <button class="btn btn-danger" type="button">Delete</button>
                                                </td>
                                            </tr>`
           })
        }
    })
    .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
    });
}

fetchTool()