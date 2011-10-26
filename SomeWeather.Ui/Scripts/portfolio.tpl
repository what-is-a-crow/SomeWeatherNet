<script type="text/html" id="thumbnail">
    <![CDATA[
	<div class="portfolioItem" id="<%= this.id %>"><img src="http://someweather.com/wp-content/themes/someweather/images/portfolio/<%= this.id %>_small.png" alt="<%= this.url %>" /></div>
	]]>
</script>

<script type="text/html" id="tooltip">
	<![CDATA[
	<div id="client_name"><%= this.name %></div>
	<% if (this.isDesign) { %>
	<div id="client_isdesign">
		<img src="http://someweather.com/wp-content/themes/someweather/images/design.png" alt="Designed by Some Weather" />
	</div>
	<% } %>
	<% if (this.isProgramming) { %>
	<div id="client_isprogramming">
		<img src="http://someweather.com/wp-content/themes/someweather/images/programming.png" alt="Programmed by Some Weather" />
	</div>
	<% } %>
	<div id="client_copy"><%= this.description %></div>
	<div id="instructions">
		( Click to View Detail )
	</div>
	]]>
</script>

<script type="text/html" id="popup">
	<![CDATA[
	<div id="close" onclick="HideFullSize();"></div>
	<img id="fullsize" src="http://someweather.com/wp-content/themes/someweather/images/portfolio/<%= this.id %>.png" />
	<% if (this.url != '') { %>
	<div id="url"><a href="<%= this.url %>" target="_blank"><%= this.url %></a></div>
	<% } %>
	<div id="close2">
		<a href="javascript:HideFullSize();">Close</a>
	</div>
	]]>
</script>