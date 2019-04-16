
    function getPersonList() {
        $.ajax({
            type: "GET",
            url: "api/v1/persons",
            success: function (data) {
                getPersonsInfo(data);
            },
            error: function (error) {
                console.log(error);
            }
        })
    }

    function removeSelectedPerson(data) {
        $.ajax({
            type: "DELETE",
            url: "api/v1/person/" + data,
            data: { id: data },
            success: function () {
                getPersonList();
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function viewSelectedPerson(data) {
        $.ajax({
            type: "GET",
            url: "api/v1/person/" + data,
            data: { id: data },
            success: function (result) {
                editModal(result);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function addNewPerson(person) {
        $.ajax({
            type: "PUT",
            url: "api/v1/person/",
            data: { Person: person },
            success: function () {
                getPersonList();
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function editPersonInfo(person) {
            $.ajax({
                type: "POST",
                url: "api/v1/person/",
                data: { Person: person },
                success: function () {
                    getPersonList();
                },
                error: function (error) {
                    console.log(error);
                }
            });
    }

    function getStringTable(data) {
        var stringData = "";

        stringData += "<tr>";
        stringData += "<td><p class=\"text-center\">" + data["name"] + "</p></td> ";
        stringData += "<td><p class=\"text-center\">" + data["displayName"] + "</p></td>";

        stringData += "<td>";
        data["skills"].forEach(function (skill) {
            stringData += '<p>';
            stringData += 'Name: ' + skill.name + ' ';
            stringData += 'Level: ' + skill.level + '<br>';
            stringData += '</p>';
        });

        stringData += "</td>";


        stringData += "<td><p class=\"text-center\"><button onclick=\"viewPerson(" + data["id"] + ")\">Edit</button></p></td>";
        stringData += "<td><p class=\"text-center\"><button onclick=\"removePerson(" + data["id"] + ")\">Remove</button></p></td>";
        stringData += "</tr>";

        return stringData;
    }

    function getPersonsInfo(data) {
        var stringTable = "";

        data.forEach(function (element) {
            stringTable += getStringTable(element);
        });

        $("#tablebody").html(stringTable);
    }

    function editModal(result) {

        $(".modal-body").html("");

        $(".modal-title").html("User data: " + result["name"]);

        var personInfo = [
            {
                Id: result["id"],
                Name: result["name"],
                DisplayName: result["displayName"]
            }
        ];

        $("#form-group-template").tmpl(personInfo).appendTo(".modal-body");

        var skillInfo = [];

        result["skills"].forEach(function (skill) {
            skillInfo.push({
                SkillId: skill.id,
                SkillName: skill.name,
                SkillLevel: skill.level
            });
        });

        $("#form-group-template-skills").tmpl(skillInfo).appendTo(".modal-body");
  
        $(".modal-body").append('</br> <button type="submit" onclick="editPerson()" class="btn btn-success">Edit Person</button>');

        $('#myModal').modal("show");
    }

    function createPersonInfo() {
        var PersonInfo = [];

        PersonInfo.push({
            Id: null,
            Name: null,
            DisplayName: null

        });

        $("#form-group-template").tmpl(PersonInfo).appendTo(".PersonInfo");
    }

    function createSkillFormGroup() {
        var skillInfo = [];

        skillInfo.push({
            SkillName: null,
            SkillLevel: null

        });

        $("#form-group-template-skills").tmpl(skillInfo).appendTo(".skills");
    }

    function editPerson() {
        var person = {};

        person.id = $(".modal-body").find("input[name = 'Id']").val();
        person.name = $(".modal-body").find("input[name = 'Name']").val();
        person.displayName = $(".modal-body").find("input[name = 'DisplayName']").val();

        person.skills = [];

        $(".modal-body .skills-form").each(function () {
            var Id = $(this).children("input[name = 'SkillId']").val();
            var Name = $(this).children("input[name = 'SkillName']").val();
            var Level = $(this).children("input[name = 'SkillLevel']").val();

            if (Name.trim() != null && Level.trim() != null) {
                person.skills.push({
                    id: Id,
                    name: Name,
                    level: Level
                });
            }
        });

        editPersonInfo(person);   

        $('#myModal').modal("hide");           
    }

    function createPerson() {
        var person = {};

        person.id = null;    
        person.name = $(".personInfo-form").children("input[name = 'Name']").val();
        person.displayName = $(".personInfo-form").children("input[name = 'DisplayName']").val();

        person.skills = [];

        $(".skills .skills-form").each(function () {

            Name = $(this).children("input[name = 'SkillName']").val();
            Level = $(this).children("input[name = 'SkillLevel']").val();

            if (Name.trim() != null && Level.trim() != null) {
                person.skills.push({
                    name: Name,
                    level: Level
                });
            }
        });

        addNewPerson(person);   
    }

    function viewPerson(data) {
        viewSelectedPerson(data);
    }

    function removePerson(data) {
        removeSelectedPerson(data);
    }

    $(document).ready(function () {

        getPersonList();

        createPersonInfo();

        $("#CreatePersonButton").click(function () {
            createPerson();
        });

        $("#CreateSkillFormGroup").click(function () {
            createSkillFormGroup();
        });       

    });