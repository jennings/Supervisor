===========
Supervisor
===========

Overview
=========

Supervisor is a monitoring and alerting system. Its purpose is to check various
settings at various intervals, and send alerts to different targets as
necessary.

For example, a DiskSpaceMonitor could exist that checks all hard drives
attached to the system once per day. If disk space drops below 10%, a message box
could be displayed on screen, or a web service could be called.


System Requirements
====================

* Microsoft .NET Framework 4.0


Source
=======

The source code is available at:

    https://github.com/jennings/Supervisor


Contributing
=============

Contributions are accepted via GitHub pull requests. As part of your pull request,
add your name to the ``NOTICE.rst`` at the root of the repository. Adding your name
to this file constitutes an agreement to license your contribution under the
terms of version 2.0 of the Apache License.
