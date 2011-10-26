var documentReady;

$(function() {			 
	LoadTwitterStatus();
	
	documentReady = true;
});

function SendMail() {
	var email = $('#txtEmail').val();
	var content = $('#txtContent').val();
	if (email == '' || content == '') 
		{
		alert('Email and Content are both required.');
		return false;
		}
	alert('Thank you. Your message is being sent.');
	return true;
}

function LoadingTick(divId, continueLoading) {
	if (continueLoading) {
		var ellipses = $('#' + divId);
		var numberOfDots = ellipses.text().length;
		var numberOfDots = (numberOfDots == 3) ? 0 : numberOfDots + 1;
		ellipses.html('');
		for (var i = 0; i < numberOfDots; i++) 
			ellipses.append('.');
			
		setTimeout('LoadingTick(\'' + divId + '\',' + continueLoading + ')', 1000);
	}
}

function Trim(text, maxChars) {
	var isTrimmed = false;
	if (text.length > maxChars)
		while (text.length + 3 > maxChars) {
			text = text.slice(0, text.length - 2);
			isTrimmed = true;
		}
	if (isTrimmed)
		text += '...';
	return text;
}

//  -----------  //
//    Twitter    //
//  -----------  //

var isLoadingTwitter = true;

function LoadTwitterStatus() {
	// tack on Twitter loader script
	$('body').append('<script src="http://twitter.com/statuses/user_timeline/someweather.json?callback=twitterCallback2&amp;count=3" type="text/javascript"></script>');

	// and while we wait on Twitter, we can watch some pretty ellipses
	setTimeout('LoadingTick(\'ellipsesTwitter\', isLoadingTwitter)', 1000);
}

function twitterCallback2(twitters) {
    if (twitters.length < 1)
    	{
    	isLoadingTwitter = false;
    	return;
    	}
    	
     var status = twitters[0].text
     						 .replace(/((https?|s?ftp|ssh)\:\/\/[^"\s\<\>]*[^.,;'">\:\s\<\>\)\]\!])/g, function(url) {
            				  	return '<a href="' + url + '">' + url + '</a>'; })
        					 .replace(/\B@([_a-z0-9]+)/ig, function(reply) {
            					return reply.charAt(0) + '<a href="http://twitter.com/' + reply.substring(1) + '">' + reply.substring(1) + '</a>'; });
    
    var statusContent = '<div class="tweet"><span>' + status + '</span>&nbsp;&nbsp;<nobr><a target="_blank" href="http://twitter.com/someweather/statuses/' 
    	+ twitters[0].id + '">' + relative_time(twitters[0].created_at) + '</a></nobr></div>';
    
    $('#gatheringupdates').fadeOut(function() { 
    	$('#twitter_updates').html(statusContent);
    	$('.tweet').fadeIn();
    });
    
    isLoadingTwitter = false;
}

function relative_time(time_value) {
    var values = time_value.split(" ");
    time_value = values[1] + " " + values[2] + ", " + values[5] + " " + values[3];
    var parsed_date = Date.parse(time_value);
    var relative_to = (arguments.length > 1) ? arguments[1] : new Date();
    var delta = parseInt((relative_to.getTime() - parsed_date) / 1000);
    delta = delta + (relative_to.getTimezoneOffset() * 60);

    if (delta < 60) {
        return 'less than a minute ago';
    } else if (delta < 120) {
        return 'about a minute ago';
    } else if (delta < (60 * 60)) {
        return (parseInt(delta / 60)).toString() + ' minutes ago';
    } else if (delta < (120 * 60)) {
        return 'about an hour ago';
    } else if (delta < (24 * 60 * 60)) {
        return 'about ' + (parseInt(delta / 3600)).toString() + ' hours ago';
    } else if (delta < (48 * 60 * 60)) {
        return '1 day ago';
    } else {
        return (parseInt(delta / 86400)).toString() + ' days ago';
    }
}