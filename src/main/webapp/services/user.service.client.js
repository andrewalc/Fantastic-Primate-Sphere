function AdminUserServiceClient() {
    this.createUser = createUser;
    this.findAllUsers = findAllUsers;
    this.findUserById = findUserById;
    this.deleteUser = deleteUser;
    this.updateUser = updateUser;
    this.url = 'https://wbdv-generic-server.herokuapp.com/api/001642349/users';
    var self = this;

    function createUser(user) {
        return fetch('https://wbdv-sp20-ryan-teng-server-jav.herokuapp.com/api/001642349/users', {
            method: 'POST',
            body: JSON.stringify(user),
            headers: {
                'content-type': 'application/json'
            }
        }).then(function(response){
            return response.json()
        })
    }

    function findAllUsers() {
        return fetch('https://wbdv-sp20-ryan-teng-server-jav.herokuapp.com/api/001642349/users')
            .then(function(response){
                return response.json()
            })
    }

    function findUserById(userId) {
        return fetch('https://wbdv-sp20-ryan-teng-server-jav.herokuapp.com/api/001642349/users/' + userId).then(function(response){
            return response.json()
        })
    }

    function updateUser(userId, user) {
        return fetch('https://wbdv-sp20-ryan-teng-server-jav.herokuapp.com/api/001642349/users/' + userId, {
            method: 'PUT',
            body: JSON.stringify(user),
            headers: {
                'content-type': 'application/json'
            }
        }).then(function(response){
            return response.json()
        })
    }
    function deleteUser(userId) {
        return fetch('https://wbdv-sp20-ryan-teng-server-jav.herokuapp.com/api/001642349/users/' + userId, {
            method: 'DELETE'
        }).then(function(response){
            return response.json()
        })
    }
}
