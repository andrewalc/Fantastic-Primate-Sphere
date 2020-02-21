
function AdminUserServiceClient() {
    this.createUser = createUser;
    this.findAllUsers = findAllUsers;
    this.findUserById = findUserById;
    this.deleteUser = deleteUser;
    this.updateUser = updateUser;
    this.url = 'https://wbdv-generic-server.herokuapp.com/api/xyz/users';
    var self = this;
    function createUser(user) {
        return fetch(self.url, {
            method: 'POST',
            body: JSON.stringify(user),
            headers: {
                'content-type': 'application/json'
            }
        })
            .then(response => response.json())
    }
    async function findAllUsers() {
        let response = await fetch(self.url)
        let users = await response.json()
        return users
        // return fetch(self.url)
        //     .then(response => response.json())
    }
    // function findUserById(userId) {
    //     return fetch(`${self.url}/${userId}`)
    //         .then(response => response.json())
    // }
    async function findUserById(userId) {
        let response = await fetch(`${self.url}/${userId}`)
        return await response.json()
    }
    // function updateUser(userId, user) {
    //     return fetch(`${self.url}/${userId}`, {
    //         method: 'PUT',
    //         body: JSON.stringify(user),
    //         headers: {
    //             'content-type': 'application/json'
    //         }
    //     })
    //         .then(response => response.json())
    // }
    async function updateUser(userId, user) {
        const response = await fetch(`${self.url}/${userId}`, {
            method: 'PUT',
            body: JSON.stringify(user),
            headers: {
                'content-type': 'application/json'
            }
        })
        return await response.json()
    }
    function deleteUser(userId) {
        return fetch(`${self.url}/${userId}`, {
            method: 'DELETE'
        })
    }
}
