# -*- coding:utf-8 -*-  

import httplib
import urllib
import website
import time

'''
1.
http://wsgs.fjaic.gov.cn/creditpub/search/ent_info_list
?
searchType=1&captcha=&session.token=585b29ac-3242-4d01-ba86-0ba2fe7d1052&condition.keyword=350581100071390"

#################################################################################################################

2.
http://wsgs.fjaic.gov.cn/creditpub/notice/view
?
uuid=Opx5m4WBYJkqRFtD6tWaYWw7xJk_JFmH&tab=01" 

'''

# 全局变量
WEBSITE_TOKEN = ""
SEARCH_KEYWORD = ""


# 验证ip
def verify_ip():
	time.sleep(1)

	url = "http://wsgs.fjaic.gov.cn/creditpub/security/verify_ip/"
	downloader = website.WebSiteDownloader()
	page = downloader.download_html_page(url)

	print page

#验证关键字
def verify_keyword():
	global SEARCH_KEYWORD

	time.sleep(1)

	url = "http://wsgs.fjaic.gov.cn/creditpub/security/verify_keyword/"
	params = SEARCH_KEYWORD
	#url = url + "?" + params

	downloader = website.WebSiteDownloader()
	page = downloader.download_html_page(url, params)

	print page

# 查询符合条件的企业列表
def do_search_ent_info_list():
	global WEBSITE_TOKEN
	global SEARCH_KEYWORD

	time.sleep(1)

	url = "http://wsgs.fjaic.gov.cn/creditpub/search/ent_info_list"
	params = "searchType=1&captcha=&session.token=%s&condition.keyword=%s" % (WEBSITE_TOKEN, SEARCH_KEYWORD)
	#url = url + "?" + params

	print url

	# if True:
	# 	return

	downloader = website.WebSiteDownloader()
	page = downloader.download_html_page(url, params)
	WEBSITE_TOKEN = website.get_token_regex(page)
	print page

	linkInfo = downloader.pick_ent_list(page)
	for urlParam in linkInfo:
		do_download_ent_info(urlParam)

# 切换页码
def turn_ent_info_list_page(pageNo):
	pass

# 下载企业信息
def do_download_ent_info(url):
	time.sleep(1)

	print url

	downloader = website.WebSiteDownloader()
	page = downloader.download_html_page(url)
	dictInfo = downloader.pick_ent_info(page)

# 初始化环境
def init_env():

	url = "http://wsgs.fjaic.gov.cn/creditpub/home/"

	downloader = website.WebSiteDownloader()
	page = downloader.download_html_page(url)

	global WEBSITE_TOKEN
	WEBSITE_TOKEN = website.get_token_regex(page)

	print WEBSITE_TOKEN

# 重置环境
def dispose_env():
	pass

def main():
	global SEARCH_KEYWORD

	SEARCH_KEYWORD = "350581100071390"

	init_env()

	result = verify_ip()

	result = verify_keyword()

	do_search_ent_info_list()

	dispose_env()

if __name__ == '__main__':
	main()