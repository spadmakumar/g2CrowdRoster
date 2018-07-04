(function () {
    // Function required for data binding
    $('#input').keyup(function (event) {
        $('#output').html($('#input').val());
    });

    var people = [
        { name: "John Smith", email: "smithj@example.com" },
        { name: "Jane Doe", email: "jane.doe@example.com" },
        { name: "Jeff Smith", email: "jsmith@mail.com" },
        { name: "Jake Doe", email: "jake@sf.com" }
    ];

    // "ng-repeat" to make the array add to the table
    $.each(people, function (index, value) {
        $('#people').append("<tr><td>" +
          value.name +
          "</td><td>" +
          value.email + "</td></tr>"
        );
    });
})();