<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="http://code.jquery.com/jquery-1.11.1.js"></script>
    <script >
        $(document).ready(function () {
            $.get("/doc.json", function (data) {
                data = JSON.parse(data);
                console.log(data);
                var descr = data.docs.rest_api_description;
                var rest_api = data.docs.rest_api;
                var container = $("#rest-api");
                container.append("<p>" + descr + "</p>")
                for (var section in rest_api) {
                    container.append("<h3>" + section + "</h3>")
                    container.append("<ul>" +
                        Object.keys(rest_api[section]).map(function (key) {
                            var article = rest_api[section][key];
                            return '<li><a href="#id_article_' + section + key + '" class="article-link">' + key + '</a>'
                                + '<div class="article-cont" '
                                + 'id="id_article_' + section + key + '" style="display:none;">'
                                + '<p>'
                                + JSON.stringify(article)
                                + '</p>' + (article.require_user == true ? '<p>Bearer token required</p>' : '')
                                + '</div></li>';
                        }).join("")
                        + "</ul>");
                }
                $(".article-link").click(function () {
                    //$(".article-cont").hide();
                    $($(this).attr('href')).toggle();
                    return false;
                });
            });
        });

    </script>


</head>
<body>
    <div id="rest-api">

    </div>
    <div id="docs-container"></div>

</body>
</html>
