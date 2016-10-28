# -*- coding:utf-8 -*-

import re
from HTMLParser import HTMLParser

class EntListPicker(HTMLParser):
    """docstring for EntListPicker"""
    def __init__(self):
        HTMLParser.__init__(self)
        self.links = []
        self.listinfo = False
        self.javascript = False
        self.token = ""

    def handle_starttag(self, tag, attrs):
        if tag == "list-info":
            self.listinfo = True
        elif tag == "a":
            if len(attrs) > 0 and self.listinfo == True:
                for k in attrs:
                    if k == "href":
                        self.links.append(attrs[k])
        elif tag == "script" and self.token == "" and self.javascript == False:
            if len(attrs) > 0:
                for k in attrs:
                    if k == "type" and attrs[k] == "text/javascript":
                        self.javascript = True

    def handle_endtag(self, tag):
       if tag == "list-info":
            self.listinfo = False

    def handle_data(self, data):
        if self.javascript != False:
            self.javascript = False        

    def getLinks(self):
        return self.links

##########################################################################
class EntInfoPicker(HTMLParser):
    DATA_NAMES = ("注册号/", "名称", "类型", "法定代表人")

    def __init__(self):
        HTMLParser.__init__(self)
        self.info = {}
        self.th = False
        self.td = False
        self.lastname = None

    def handle_starttag(self, tag, attrs):
        if tag == "th":
            self.th = True
            return
        elif tag == "td":
            self.td = True

    def handle_endtag(self, tag):
        if tag == "th":
            self.th = False
            return
        elif tag == "td":
            self.td = False

    def handle_data(self, data):
        if self.th == True:
            if data in self.DATA_NAMES:
                if not self.info.has_key(data):
                    self.info[data] = ""
                    self.lastname = data
        elif self.td == True:
            if self.lastname != None \
            and self.info.has_key(self.lastname) == True \
            and self.info[self.lastname] == "":
                self.info[self.lastname] = data
                self.lastname = None


    def getInfo(self):
        for k in self.info:
            print k, self.info[k]
        return self.info
