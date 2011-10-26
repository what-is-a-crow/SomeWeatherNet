var clients;
var isLoadingPortfolio = true;
var templates = {};
var template = null;
var url = null;

$(function() {
	//setTimeout('LoadingTick(\'ellipsesPortfolio\', isLoadingPortfolio)', 1000);
	toolTip = $('#tooltip');

//	$.get(template, function(doc) {
//		// load templates into memory
//		var tmpls = $(doc).filter('script');
//		tmpls.each(function() {
//			templates[this.id] = $.jqotec(this);
//		});
//		
//		// fetch portfolio items
//		 $.getJSON(url, function(data) {
//			$('.loading').fadeOut(function() {
//				clients = data;
//				// display items
//				$('#portfolio')
//					.jqoteapp(templates.thumbnail, data)
//					.append('<div class="clear"></div>');
//				
//				WireUpEventHandlers();
//				isLoadingPortfolio = false;
//			});
//		});
//	});
});

function WireUpEventHandlers() {
	$('.portfolioItem').bind('mouseenter', function(e) { ShowToolTip(e, $(this).attr('id')); })
					   .bind('mousemove', function(e) { MoveToolTip(e); })
					   .bind('mouseleave', function(e) {  HideToolTip(e, false); })
					   .bind('click', function(e) { return ShowFullSize($(this).attr('id')); });
						   				   
	$('#tooltip').bind('mouseleave', function(e) { HideToolTip(e, false); });
	
	$('#fullsize').bind('mouseover', function() { $('#details').fadeIn(); })
				  .bind('mouseout', function() { $('#details').fadeOut(); });
}

function GetClient(id) {
	var client = null;
	for (var i = 0; i < clients.length; i++)
		if (clients[i].id == id) {
			client = clients[i];
			break;
		}
	return client;
}

var fullSizeVisible;
function ShowFullSize(id) {
	var client = GetClient(id);
	if (client == null) return;
	
	fullSizeVisible = true;
	HideToolTip(null, true);
	
	var popup = $('#popup');
	popup.html('').jqoteapp(templates.popup, client);
	var body = $('body');
	// popup not visible; can't get measurements
	var popupHeight = 712;
	var popupWidth = 665;
	var top = (body.height() > popupHeight) ? 20 : 12;
    var left = Math.max(0, (body.width() - popupWidth) / 2);
    popup.css('top', top + 'px')
    	 .css('left', left + 'px');
    $('#overlay').fadeIn('slow', function() { popup.fadeIn('slow'); });
}

function HideFullSize() {
	fullSizeVisible = false;
	$('#popup').fadeOut('slow', function() { $('#overlay').fadeOut('slow'); });
}


//  -----------  //
//    Tooltip    //
//  -----------  //

var toolTip;
var toolTipShown;
var isAtRightEdge;
var isAtBottomEdge;
var toolTipOffsetX = 20;
var crownHeight = 82;
var toolTipWidth = 320;

function ShowToolTip(e, id) {
    // don't show tooltip if document is not loaded
    if (documentReady && !fullSizeVisible) {
        toolTipShown = true;

		var client = GetClient(id);
		$('#portfolio_detail')
			.html('')
			.jqoteapp(templates.tooltip, client);

        // show tooltip with text
        toolTip.show();
    }
}

function MoveToolTip(e) {
    if (toolTipShown && documentReady && !fullSizeVisible) {
        isAtRightEdge = (e.clientX + toolTipOffsetX + toolTipWidth) > $(window).width();
        var toolTipOffsetY = isAtRightEdge ? 20 : -(crownHeight);
        isAtBottomEdge = (e.clientY + toolTipOffsetY + toolTip.outerHeight()) > $(window).height();
        
        if (isAtRightEdge) {
            toolTip.css('left', 'auto')
            	   .css('right', -$(window).scrollLeft() + 'px');
            if (!isAtBottomEdge) {
            	toolTip.css('top', (e.pageY + toolTipOffsetY) + 'px')
            		   .css('bottom', 'auto');
            }
        }
        if (isAtBottomEdge) {
        	toolTip.css('top', 'auto');
        	toolTip.css('bottom', -$(window).scrollTop() + 'px');
        	if (!isAtRightEdge) {
        		toolTip.css('left', (e.pageX + toolTipOffsetX) + 'px');
        	}
        }
        if (!isAtRightEdge && !isAtBottomEdge) {
            toolTip.css('bottom', 'auto')
            	   .css('right', 'auto')
            	   .css('left', (e.pageX + toolTipOffsetX) + 'px')
                   .css('top', (e.pageY - crownHeight) + 'px');
        }
        
       	AdjustToolTipWidth();
    }
}

function HideToolTip(e, forceHide) {
    if (typeof (toolTip) == 'undefined' || (typeof (e) == 'undefined' && !forceHide)) return;
    
    if (!forceHide && IsMouseOverToolTip(e)) return;
    
    toolTip.css('left', "-1000px").css('right', 'auto').css('width', 'auto');
   
   	AdjustToolTipWidth();
}

function AdjustToolTipWidth() {
	// if IE7, tooltip's width goes all wonky.. this is a workaround
	if (toolTip.width() != toolTipWidth)
    	toolTip.width(toolTipWidth);
}

function IsMouseOverToolTip(e){
    var offset = toolTip.offset();
    var isOverToolTip =  (e.pageX > offset.left && isAtRightEdge &&
    	e.pageY > offset.top && isAtBottomEdge);
    return isOverToolTip;
}