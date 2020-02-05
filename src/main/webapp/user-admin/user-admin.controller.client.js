(function () {
    var $usernameFld;
    var $passwordFld;
    var $firstNameFld;
    var $lastNameFld;
    var $roleFld;
    var $removeBtn;
    var $editBtn;
    var $createBtn;
    var $updateBtn;
    var $userRowTemplate;
    var $form;
    var $tbody;
    var $id;
    var $currentUser = null;

    var userService = new AdminUserServiceClient();

    function createUser() {
        $usernameFld = $('#usernameFld');
        $passwordFld = $('#passwordFld');
        $firstNameFld = $('#firstNameFld');
        $lastNameFld = $('#lastNameFld');
        $roleFld = $('#roleFld');

        $id = Math.floor(Math.random() * 1000000);
        var username = $usernameFld.val();
        var password = $passwordFld.val();
        var firstName = $firstNameFld.val();
        var lastName = $lastNameFld.val();
        var role = $roleFld.val();

        var user = {
            id: $id,
            username: username,
            password: password,
            firstName: firstName,
            lastName: lastName,
            role: role
        };
        userService.createUser(user).then(renderUsers).then(clearFields);
    }

    function clearFields() {
        $('#usernameFld').val('');
        $('#passwordFld').val('');
        $('#firstNameFld').val('');
        $('#lastNameFld').val('');
        $('#roleFld').val('FACULTY');
    }

    function findAllUsers() {
        userService.findAllUsers().then(renderUsers)
    }

    function findUserById(event) {
        userService.findUserById(event.data.id).then(updateUser)
    }

    function deleteUser(event) {
        userService.deleteUser(event.data.id).then(renderUsers)
    }

    function selectUser(event) {
        $('#usernameFld').val(event.data.username);
        $('#passwordFld').val(event.data.password);
        $('#firstNameFld').val(event.data.firstName);
        $('#lastNameFld').val(event.data.lastName);
        $('#roleFld').val(event.data.role);

        $currentUser = {
            id: event.data.id,
            username: event.data.username,
            password: event.data.password,
            firstName: event.data.firstName,
            lastName: event.data.lastName,
            role: event.data.role
        }
    }

    function updateUser() {
        if ($currentUser) {
            var user = {
                id: $currentUser.id,
                username: $('#usernameFld').val(),
                password: $('#passwordFld').val(),
                firstName: $('#firstNameFld').val(),
                lastName: $('#lastNameFld').val(),
                role: $('#roleFld').val()
            };
            userService.updateUser($currentUser.id, user).then(renderUsers).then(clearFields);
            $currentUser = null;
        }
    }

    function renderUser(user) {
        $tbody.find()
    }

    function renderUsers(users) {
        $tbody.empty();
        for (var u in users) {
            var user = users[u];
            var newRow = $userRowTemplate.clone();
            newRow.find('.wbdv-username').html(user.username);
            newRow.find('.wbdv-password').html(user.password);
            newRow.find('.wbdv-first-name').html(user.firstName);
            newRow.find('.wbdv-last-name').html(user.lastName);
            newRow.find('.wbdv-role').html(user.role);
            $editBtn = $("<button class=\"btn\"><i id=\"wbdv-edit\" class=\"fa-2x fa fa-pencil-alt wbdv-edit\"></i></button>").clone();
            $removeBtn = $("<button class=\"btn\"><i id=\"wbdv-remove\" class=\"fa-2x fa fa-times wbdv-remove\"></i></button>").clone();
            $editBtn.on("click",{
                id: user.id,
                username: user.username,
                password: user.password,
                firstName: user.firstName,
                lastName: user.lastName,
                role: user.role
            }, selectUser
            );
            $removeBtn.on("click", {
                id: user.id
            }, deleteUser
            );

            newRow.find('.wbdv-actions').append($editBtn).append($removeBtn);
            $tbody.append(newRow);
        }
    }

    jQuery(main);

    function main() {
        userService.findAllUsers().then(renderUsers);
        $createBtn = $('.wbdv-create');
        $updateBtn = $('.wbdv-update');

        $userRowTemplate = $('.wbdv-template');
        $form = $('.wbdv-form');
        $tbody = $('.wbdv-tbody');

        $createBtn.click(createUser);
        $updateBtn.click(updateUser);
    }

})();

