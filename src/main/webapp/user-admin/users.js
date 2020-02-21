

(function() {
    let userService = new AdminUserServiceClient()

    let $userList
    let $usernameFld
    let $createBtn
    let $updateBtn

    let users = [
        {username: 'alice'},
        {username: 'bob'}
    ]

    function deleteUser(index) {
        let user = users[index]
        let userId = user._id

        userService.deleteUser(userId)
            .then(response => {
                users.splice(index, 1)
                renderUsers()
            })
    }

    function createUser() {
        const newUser = {
            username: $usernameFld.val()
        }
        $usernameFld.val("")

        userService.createUser(newUser)
            .then((actualUser) => {
                // users.push(actualUser)
                // renderUsers()
                findAllUsers()
            })
    }

    let currentUserIndex = -1
    async function editUser(index) {
        currentUserIndex = index
        let user = users[index]
        let userId = user._id

        const actualUser = await userService.findUserById(userId)
        $usernameFld.val(actualUser.username)
    }

    async function updateUser() {
        const updatedUser = {
            username: $usernameFld.val()
        }
        $usernameFld.val("")
        updatedUser._id = users[currentUserIndex]._id

        const actualUser = await userService.updateUser(updatedUser._id, updatedUser)
        findAllUsers()
    }

    function renderUsers() {
        $userList.empty()
        for(let u in users) {
            let user = users[u]

            $deleteBtn = $("<button>Delete</button>")
            $deleteBtn.click(() => deleteUser(u))

            $editBtn = $("<button>Edit</button>")
            $editBtn.click(() => editUser(u))

            $li = $("<li>")
            $li.append($deleteBtn)
            $li.append($editBtn)
            $li.append(user.username)
            $userList.append($li)
        }
    }
    async function findAllUsers() {
        // userService
        //     .findAllUsers()
        //     .then(theusers => {
        //             users = theusers
        //             renderUsers()
        // })
        users = await userService.findAllUsers();
        renderUsers();
    }
    function main() {
        $userList = $("#userList")
        $usernameFld = $("#usernameFld")
        $createBtn = $("#createBtn")
        $createBtn.click(createUser)
        $updateBtn = $("#updateBtn")
        $updateBtn.click(updateUser)

        findAllUsers()

        // userService
        //     .findAllUsers()
        //     .then(theusers => {
        //         users = theusers
        //         renderUsers()
        //     })
    }
    $(main)
})()
