import utility.CloneGitRepo


def main():
    repoURL = "git@github.cds.internal.unity3d.com:unity/xr.oculus.tests.git"
    branch = "2019.4-xrsdk"
    utility.CloneGitRepo.CloneGitRepo_GitHub(repoURL, branch)


if __name__ == "__main__":
    main()
