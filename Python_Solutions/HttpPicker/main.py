# -*- coding:utf-8 -*-  

import urllib

from WebSiteDownloader import WebSiteDownloader

search_url = "http://wsgs.fjaic.gov.cn/creditpub/home"
search_ent_id = ""

search_server = "wsgs.fjaic.gov.cn"
search_remote_url = "http://wsgs.fjaic.gov.cn/creditpub/search/ent_info_list"
search_param = {'formInfo':'nd'}

# 下载企业信息
def do_download_ent_info(url = None):
	url = url if url != None else "http://wsgs.fjaic.gov.cn/creditpub/notice/view?uuid=Opx5m4WBYJkqRFtD6tWaYWw7xJk_JFmH&tab=01"
	downloader = WebSiteDownloader()
	page = downloader.download_html_page(url)
	dictInfo = downloader.pick_ent_info(page)

# 查询导出企业列表
def do_download_ent_list():
	url = "http://wsgs.fjaic.gov.cn/creditpub/search/ent_info_list"
	params = {'formInfo':'nd'}
	targets = urllib.urlencode(params)
	downloader = WebSiteDownloader()
	page = downloader.download_html_page(url, targets)
	print page
	linkInfo = downloader.pick_ent_list(page)
	for i in linkInfo:
		do_download_ent_info(i)

def main():
	do_download_ent_list()

if __name__ == '__main__':
	main()