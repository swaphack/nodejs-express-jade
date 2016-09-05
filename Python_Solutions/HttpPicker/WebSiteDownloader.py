# -*- coding:utf-8 -*-  

import httplib
import urllib
import urllib2
import os


from Picker import EntInfoPicker
from Picker import EntListPicker

#########################################################
class WebSiteDownloader(object):
	"""docstring for WebSiteDownload"""
	def __init__(self):
		super(WebSiteDownloader, self).__init__()

	def do_get_request(self, dns, url):
		conn = httplib.HTTPConnection(dns)
		conn.request("GET", url)
		resp = conn.getresponse()
		text = resp.read()
		conn.close()
		return text

	def do_post_request(self, dns, url, params):
		postParams = urllib.urlencode(params)
		conn = httplib.HTTPConnection(dns)
		conn.request("POST", url, postParams)
		resp = conn.getresponse()
		text = resp.read()
		conn.close()
		return text
		
	# 下载页面
	def download_html_page(self, url, data = None):
		req = urllib2.Request(url, data)
		response = urllib2.urlopen(req)
		pageContent = response.read()
		return pageContent

	# 保存到本地
	def save_to_local(self, filepath, pageContent):
		fptr = file(filepath, "wb+")
		fptr.write(pageContent)
		fptr.close()

	# 获取企业列表
	def pick_ent_list(self, pageContent):
		picker = EntListPicker()
		picker.feed(pageContent)
		picker.close()
		return picker.getLinks()

	# 获取企业信息
	def pick_ent_info(self, pageContent):
		picker = EntInfoPicker()
		picker.feed(pageContent)
		picker.close()
		return picker.getInfo()
