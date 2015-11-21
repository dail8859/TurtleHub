"""
Export svg files to images needed for TurtleHub.
"""

import subprocess as sp

exe_path = "C:\Program Files\Inkscape\inkscape.exe"
fname = "turtle_hub-%d.png"
sizes = [16, 32, 64, 128]


def generate_logos():
    cmd = exe_path + " --export-png " + fname + " -w %d turtlehub_logo.svg"
    for w in sizes:
        print "Exporting logo size %dx%d..." % (w, w)
        sp.call(cmd % (w, w))

def generate_ico():
    print "Generating icon..."
    cmd = "png2ico.exe TurtleHub.ico " + " ".join(fname % (w) for w in sizes)
    sp.call(cmd)

def generate_banner():
    print "Exporting banner..."
    cmd = exe_path + " --export-png ../src/Setup/Banner.png -w 493 turtlehub_banner.svg"
    sp.call(cmd)

def generate_dialog():
    print "Exporting dialog..."
    cmd = exe_path + " --export-png ../src/Setup/Dialog.png -w 493 turtlehub_dialog.svg"
    sp.call(cmd)

if __name__ == "__main__":
    generate_logos()
    generate_ico()
    generate_banner()
    generate_dialog()
